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

    public Vector3 destination;
    public int movSpeed;
    public State state;

    private NavMeshAgent agent;

    private SelectedDictionary selectedTable;
    private ResourceDictionary resourcesInWorld;
    private List<GameObject> sorted;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        selectedTable = EventSystem.current.GetComponent<SelectedDictionary>();
        resourcesInWorld = EventSystem.current.GetComponent<ResourceDictionary>();
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
            case State.NAVIGATE_TO_RESOURCE:
                NavigateToResource();
                break;
            case State.DELIVERING_TO_STORAGE:
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
        if (resourcesInWorld.Available())
        {
            state = State.NAVIGATE_TO_RESOURCE;
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

    private void NavigateToResource()
    {
        destination = resourcesInWorld.GetTable().ToList().First().Value.gameObject.transform.position;
        agent.SetDestination(destination);

        PickupResource();
    }

    void PickupResource()
    {
        foreach (var item in resourcesInWorld.GetTable().ToList())
        {
            //If distance to resource is smaller than given value, pick up item
            if (Vector3.Distance(gameObject.transform.position, item.Value.transform.position) < 5)
            {
                //Remove from the world resources dictionary
                resourcesInWorld.RemoveFromTable(item.Value);

                //Add to inventory dictionary
                if (inventory.ContainsKey(item.Value.GetInstanceID()))
                {
                    inventory.Add(item.Value.GetInstanceID(), item.Value);
                }

                //Carry resource ingame


                state = State.DELIVERING_TO_STORAGE;
            }
        }
    }

    void DeliverToStorage()
    {

    }

    //Sort list based on distance from the NPC, so npc always goes to closest target
    void SortDestinations(GameObject toSave)
    {
        sorted.Add(toSave);
        sorted = sorted.OrderBy(s => Vector3.Distance(gameObject.transform.position, s.transform.position)).ToList();
    }
}
