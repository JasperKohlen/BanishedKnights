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
    public GameObject destination;
    public int movSpeed;
    private State state;

    private NavMeshAgent agent;
    private SelectedDictionary selectedTable;
    private List<GameObject> sorted;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        selectedTable = EventSystem.current.GetComponent<SelectedDictionary>();
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
            case State.BUILDING:
                break;
            case State.DELIVERING:
                break;
            case State.WORKING:
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
        destination = sorted.First();

        agent.SetDestination(destination.transform.position);
        sorted.Clear();
    }

    //Sort list based on distance from the NPC, so npc always goes to closest target
    void SortDestinations(GameObject toSave)
    {
        sorted.Add(toSave);
        sorted = sorted.OrderBy(s => Vector3.Distance(gameObject.transform.position, s.transform.position)).ToList();
    }
}
