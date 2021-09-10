using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class Worker : MonoBehaviour
{
    [HideInInspector] public ResourceDictionary resourceToDeliver;
    private SelectedDictionary selectedTable;
    private StorageBuildingsDictionary storages;
    private ToBuildDictionary structs;
    [HideInInspector] public WorkerInventory inventory;

    private List<GameObject> sortedResources;
    private List<GameObject> sortedStorages;

    private Vector3 destination;
    public State state;
    [SerializeField]
    private int movSpeed;

    private NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        selectedTable = EventSystem.current.GetComponent<SelectedDictionary>();
        storages = EventSystem.current.GetComponent<StorageBuildingsDictionary>();
        structs = EventSystem.current.GetComponent<ToBuildDictionary>();

        resourceToDeliver = gameObject.AddComponent<ResourceDictionary>();
        inventory = gameObject.AddComponent<WorkerInventory>();

        sortedResources = new List<GameObject>();
        sortedStorages = new List<GameObject>();

        agent.speed = movSpeed;
        state = State.IDLE;
    }

    // Update is called once per frame
    void Update()
    {
        HandleState();
        switch (state)
        {
            case State.IDLE:
                Idling();
                break;
            case State.REMOVING:
                NavigateToRemovable();
                break;
            case State.DESTROYING:
                break;
            case State.DELIVERING_TO_BUILD:
                DeliverToBuild();
                break;
            case State.DELIVERING_TO_STORAGE:
                DeliverToStorage();
                break;
            default:
                HandleState();
                break;
        }
    }
    private void HandleState()
    {
        if (selectedTable.GetTable().Count == 0 && resourceToDeliver.GetTable().Count == 0)
        {
            state = State.IDLE;
        }
        if (selectedTable.GetTable().Count > 0 && resourceToDeliver.GetTable().Count == 0)
        {
            state = State.REMOVING;
        }
        if (resourceToDeliver.GetTable().Count() > 0)
        {
            state = State.DELIVERING_TO_STORAGE;
        }
        if (structs.Available() && inventory.HoldingResource())
        {
            state = State.DELIVERING_TO_BUILD;
        }
    }
    private void Idling()
    {
        agent.SetDestination(gameObject.transform.position);
    }

    //Finds needed resource and delivers it to a construction site
    void DeliverToBuild()
    {
        PickupResource(resourceToDeliver.Get());

        //TODO: Get Resources if not holding and available in storages
        //TODO: algorithm that makes sure you cant drop off more resources than needed
        // and that checks which resources are still required

        destination = structs.GetTable().First().Value.transform.position;
        agent.SetDestination(destination);
    }
    public void PlaceResourceToBuild()
    {
        //Place near building
        resourceToDeliver.Get().transform.parent = null;
        resourceToDeliver.Get().transform.position = gameObject.transform.position;

        //Remove from worker inventory
        inventory.RemoveFromTable(resourceToDeliver.Get());
        resourceToDeliver.RemoveFromTable(resourceToDeliver.Get());
    }

    //Called when in the DELIVERING_TO_STORAGE state
    void DeliverToStorage()
    {
        PickupResource(resourceToDeliver.Get());

        //Set destination to nearest storage
        NavigateToStorage();
    }
    public void PickupResource(GameObject resource)
    {
        Debug.Log("PICKUP");
        //Carry resource ingame
        resource.GetComponent<Rigidbody>().useGravity = false;
        resource.GetComponent<Rigidbody>().isKinematic = true;
        resource.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 3, gameObject.transform.position.z);
        resource.transform.SetParent(gameObject.transform, false);

        //Place in worker inventory 
        inventory.Add(resource);
    }

    //Called when entering a storage trigger when holding a resource to deliver
    public void DropInStorage(LocalStorageDictionary storageInv)
    {
        Debug.Log("Dropping in storage...");

        //Place in storage
        storageInv.Add(resourceToDeliver.Get());

        //Remove from worker inventory
        inventory.RemoveFromTable(resourceToDeliver.Get());

        //Remove from the toDeliver dictionary
        Destroy(resourceToDeliver.GetTable().ToList().First().Value.gameObject);
        resourceToDeliver.RemoveFromTable(resourceToDeliver.Get());
    }

    #region Sorting
    //Sort list based on distance from the NPC, so npc always goes to closest target
    void SortDestinations(GameObject toSave)
    {
        sortedResources.Add(toSave);
        sortedResources = sortedResources.OrderBy(s => Vector3.Distance(gameObject.transform.position, s.transform.position)).ToList();
    }
    void NavigateToRemovable()
    {
        foreach (var item in selectedTable.GetTable())
        {
            SortDestinations(item.Value);
        }
        destination = sortedResources.First().transform.position;

        agent.SetDestination(destination);
        sortedResources.Clear();
    }

    void SortStoragesByDistance(GameObject toSave)
    {

        sortedStorages.Add(toSave);
        sortedStorages = sortedStorages.OrderBy(s => Vector3.Distance(gameObject.transform.position, s.transform.position)).ToList();
    }

    void NavigateToStorage()
    {
        foreach (var item in storages.GetTable())
        {
            SortStoragesByDistance(item.Value);
        }

        destination = sortedStorages.First().transform.position;

        agent.SetDestination(destination);
        sortedStorages.Clear();
    }
    #endregion
}
