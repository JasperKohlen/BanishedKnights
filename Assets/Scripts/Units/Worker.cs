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
    private string neededResource;
    public State state;
    [SerializeField]
    private int movSpeed;

    private NavMeshAgent agent;
    private bool isStorage;
    private bool isCollecting;

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
        switch (ReturnState())
        {
            case State.IDLE:
                Idling();
                break;
            case State.REMOVING:
                NavigateToRemovable();
                break;
            case State.DESTROYING:
                break;
            case State.COLLECTING_TO_BUILD:
                NavigateToCollect();
                break;
            case State.DELIVERING_TO_BUILD:
                DeliverToBuild();
                break;
            case State.DELIVERING_TO_STORAGE:
                PrepareToDeliverToStorage();
                break;
            default:
                ReturnState();
                break;
        }
    }

    private State ReturnState()
    {
        if (resourceToDeliver.GetTable().Count() > 0)
        {
            PickupResource(resourceToDeliver.Get());
        }
        if (selectedTable.GetTable().Count > 0 && resourceToDeliver.GetTable().Count == 0)
        {
            state = State.REMOVING;
            return state;
        }
        if (structs.Available() && inventory.HoldingResource() && !isStorage)
        {
            state = State.DELIVERING_TO_BUILD;
            return state;
        }
        if (structs.Available() && !inventory.HoldingResource())
        {
            state = State.COLLECTING_TO_BUILD;
            return state;
        }
        if (resourceToDeliver.GetTable().Count > 0 || isStorage)
        {
            state = State.DELIVERING_TO_STORAGE;
            return state;
        }

        state = State.IDLE;
        return State.IDLE;
    }
    private void Idling()
    {
        agent.SetDestination(gameObject.transform.position);
    }

    //Get Resources from storage if not holding and available in storages
    private void NavigateToCollect()
    {
        agent.SetDestination(FindStorageToCollectFrom());
    }
    //Called when entering storage trigger and worker is in the collecting_to_build state
    public void CollectResource(LocalStorageDictionary storageInv)
    {
        resourceToDeliver.Add(storageInv.ReturnResource(neededResource));
        PickupResource(resourceToDeliver.Get());
        storageInv.Remove(resourceToDeliver.Get());
    }
    //Finds needed resource and delivers it to a construction site
    void DeliverToBuild()
    {
        //PickupResource(resourceToDeliver.Get());

        if (CanDeliverLogsToBuild())
        {
            destination = FindDeliverLogsToBuild();
        }
        if (CanDeliverCobblesToBuild())
        {
            destination = FindDeliverCobblesToBuild();
        }
        if (ShouldDeliverToStorage())
        {
            isStorage = true;
        }

        agent.SetDestination(destination);
    }

    //Called when entering onTrigger on blueprint while holding resource
    public void PlaceResourceToBuild()
    {
        //Place near building
        resourceToDeliver.Get().transform.parent = null;
        resourceToDeliver.Get().transform.position = gameObject.transform.position;

        //Remove from worker inventory
        inventory.Remove(resourceToDeliver.Get());
        resourceToDeliver.Remove(resourceToDeliver.Get());
    }

    //Called when in the DELIVERING_TO_STORAGE state
    void PrepareToDeliverToStorage()
    {
        //Set destination to nearest storage
        agent.SetDestination(GetNearestStorage());
    }

    private void PickupResource(GameObject resource)
    {
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
        inventory.Remove(resourceToDeliver.Get());

        //Remove from the toDeliver dictionary
        resourceToDeliver.GetTable().ToList().First().Value.gameObject.transform.position = new Vector3(9999, 9999, 9999);
        resourceToDeliver.Remove(resourceToDeliver.Get());
        isStorage = false;
    }

    #region navigation
    //Find the closest structure that still needs cobbles
    private Vector3 FindDeliverCobblesToBuild()
    {
        List<Vector3> blueprints = new List<Vector3>();
        Vector3 structToDeliverTo = transform.position;

        foreach (var item in structs.GetTable())
        {
            if (!item.Value.GetComponent<StructureBuild>().AllCobblesDelivered())
            {
                blueprints.Add(item.Value.transform.position);
            }
        }
        blueprints = blueprints.OrderBy(s => Vector3.Distance(gameObject.transform.position, s)).ToList();

        return blueprints.First();
    }

    //Find the closest structure that still needs logs
    private Vector3 FindDeliverLogsToBuild()
    {
        List<Vector3> blueprints = new List<Vector3>();
        Vector3 structToDeliverTo = transform.position;

        foreach (var item in structs.GetTable())
        {
            if (!item.Value.GetComponent<StructureBuild>().AllLogsDelivered())
            {
                blueprints.Add(item.Value.transform.position);
            }
        }
        blueprints = blueprints.OrderBy(s => Vector3.Distance(gameObject.transform.position, s)).ToList();

        return blueprints.First();
    }

    private Vector3 FindStorageToCollectFrom()
    {
        Vector3 destination = transform.position;
        foreach (var toBuild in structs.GetTable())
        {
            foreach (var storage in storages.GetTable())
            {
                //If a buildable requires logs and a storage house has atleast one log
                if (!toBuild.Value.GetComponent<StructureBuild>().AllLogsDelivered() && storage.Value.GetComponent<LocalStorageDictionary>().GetLogsCount() > 0)
                {
                    neededResource = "Logs";
                    destination = storage.Value.transform.position;
                    break;
                }
                //If a buildable requires cobbles and a storage house has atleast one cobble
                if (!toBuild.Value.GetComponent<StructureBuild>().AllCobblesDelivered() && storage.Value.GetComponent<LocalStorageDictionary>().GetCobblesCount() > 0)
                {
                    neededResource = "Cobbles";
                    destination = storage.Value.transform.position;
                    break;
                }
            }
        }
        return destination;
    }

    #endregion

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

    Vector3 GetNearestStorage()
    {
        foreach (var item in storages.GetTable())
        {
            SortStoragesByDistance(item.Value);
        }

        destination = sortedStorages.First().transform.position;
        sortedStorages.Clear();

        return destination;
    }
    #endregion

    #region booleans

    private bool CanDeliverLogsToBuild()
    {
        if (resourceToDeliver.Get().name.Contains("Logs") && structs.GetTable().Any(s => s.Value.GetComponent<StructureBuild>().AllLogsDelivered() == false))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool CanDeliverCobblesToBuild()
    {
        if (resourceToDeliver.Get().name.Contains("Cobbles") && structs.GetTable().Any(s => s.Value.GetComponent<StructureBuild>().AllCobblesDelivered() == false))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    private bool ShouldDeliverToStorage()
    {
        if (structs.GetTable().Count == 0)
        {
            return true;
        }
        if (CanDeliverLogsToBuild() || CanDeliverCobblesToBuild())
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    #endregion
}
