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
    private Dictionary<int, GameObject> inventory;

    [HideInInspector] 
    public ResourceDictionary resourcesToDeliver;
    private SelectedDictionary selectedTable;
    private StorageBuildingsDictionary storages;
    private List<GameObject> sorted;

    private Vector3 destination;
    [HideInInspector]
    private bool delivering;
    public State state;
    [SerializeField]
    private int movSpeed;

    private NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        delivering = false;
        agent = GetComponent<NavMeshAgent>();
        selectedTable = EventSystem.current.GetComponent<SelectedDictionary>();
        storages = EventSystem.current.GetComponent<StorageBuildingsDictionary>();

        resourcesToDeliver = gameObject.AddComponent<ResourceDictionary>();
        inventory = new Dictionary<int, GameObject>();
        sorted = new List<GameObject>();

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
        if (selectedTable.GetTable().Count == 0 && resourcesToDeliver.GetTable().Count == 0)
        {
            state = State.IDLE;
        }
        if (selectedTable.GetTable().Count > 0 && resourcesToDeliver.GetTable().Count == 0)
        {
            state = State.REMOVING;
        }
        if (resourcesToDeliver.GetTable().Count() > 0)
        {
            state = State.DELIVERING_TO_STORAGE;
        }
        //else
        //{
        //    state = State.IDLE;
        //}
    }
    private void Idling()
    {
        agent.SetDestination(gameObject.transform.position);
    }

    void NavigateToRemovable()
    {
        foreach (var item in selectedTable.GetTable())
        {
            SortDestinations(item.Value);
        }
        destination = sorted.First().transform.position;

        agent.SetDestination(destination);
        sorted.Clear();
    }

    void PickupResource(GameObject resource)
    {
        //Place in worker inventory BUGGED
        //inventory.Add(resource.GetInstanceID(), resource);

        //Carry resource ingame
        resource.GetComponent<Rigidbody>().useGravity = false;
        resource.GetComponent<Rigidbody>().isKinematic = true;
        resource.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 3, gameObject.transform.position.z);
        resource.transform.SetParent(gameObject.transform, false);
    }

    //Called when in the DELIVERING_TO_STORAGE state
    void DeliverToStorage()
    {
        foreach (var item in resourcesToDeliver.GetTable().ToList())
        {
            PickupResource(item.Value.gameObject);

            //Set destination to nearest storage
            agent.SetDestination(storages.GetTable().ToList().First().Value.transform.position);
        }
    }

    //Called when entering a storage building when holding a resource to deliver
    public void DropInStorage(LocalStorageDictionary storageInv)
    {
        Debug.Log("Dropping in storage...");

        storageInv.Add(resourcesToDeliver.GetTable().ToList().First().Value.gameObject);
        Debug.Log("Items in local storage: " + storageInv.GetTable().Count);

        //Remove from the toDeliver dictionary
        Destroy(resourcesToDeliver.GetTable().ToList().First().Value.gameObject);
        resourcesToDeliver.RemoveFromTable(resourcesToDeliver.GetTable().ToList().First().Value.gameObject);
        //remove from inventory
    }

    //Sort list based on distance from the NPC, so npc always goes to closest target
    void SortDestinations(GameObject toSave)
    {
        sorted.Add(toSave);
        sorted = sorted.OrderBy(s => Vector3.Distance(gameObject.transform.position, s.transform.position)).ToList();
    }
}
