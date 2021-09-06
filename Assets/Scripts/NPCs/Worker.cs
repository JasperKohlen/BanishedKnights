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

    private Vector3 destination;
    [SerializeField] private int movSpeed;
    private bool delivering;
    [HideInInspector] public State state;

    private NavMeshAgent agent;

    private SelectedDictionary selectedTable;
    [HideInInspector] public ResourceDictionary resourcesToDeliver;
    private StorageDictionary storages;

    private List<GameObject> sorted;

    // Start is called before the first frame update
    void Start()
    {
        delivering = false;
        agent = GetComponent<NavMeshAgent>();
        selectedTable = EventSystem.current.GetComponent<SelectedDictionary>();
        resourcesToDeliver = EventSystem.current.GetComponent<ResourceDictionary>();
        storages = EventSystem.current.GetComponent<StorageDictionary>();
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
        if (selectedTable.GetTable().Count == 0)
        {
            state = State.IDLE;
        }
        if (selectedTable.GetTable().Count > 0)
        {
            state = State.REMOVING;
        }
        if (resourcesToDeliver.GetTable().Count() > 0)
        {
            state = State.DELIVERING_TO_STORAGE;
        }
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
        Debug.Log("Picking up...");
        Debug.Log(state);

        //Remove from the toDeliver dictionary
        resourcesToDeliver.RemoveFromTable(resource);

        //Place in worker inventory
        inventory.Add(resource.GetInstanceID(), resource);
        Debug.Log(inventory.Count());

        //Carry resource ingame
        resource.transform.SetParent(gameObject.transform);
        resource.transform.position = new Vector3(0,4,0);
    }

    void DeliverToStorage()
    {
        foreach (var item in resourcesToDeliver.GetTable().ToList())
        {
            destination = item.Value.gameObject.transform.position;
            agent.SetDestination(destination);

            PickupResource(item.Value.gameObject);

            while (delivering)
            {
                //Set destination to nearest storage
                agent.SetDestination(storages.GetTable().ToList().First().Value.transform.position);
                delivering = false;
            }
        }
    }

    //Sort list based on distance from the NPC, so npc always goes to closest target
    void SortDestinations(GameObject toSave)
    {
        sorted.Add(toSave);
        sorted = sorted.OrderBy(s => Vector3.Distance(gameObject.transform.position, s.transform.position)).ToList();
    }
}
