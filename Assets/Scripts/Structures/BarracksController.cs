using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BarracksController : MonoBehaviour
{
    [SerializeField] private GameObject logs;
    [SerializeField] private GameObject cobbles;

    private Worker worker;
    private OrderDictionary orders;
    private LocalStorageDictionary barracksInv;
    private void Start()
    {
        orders = EventSystem.current.GetComponent<OrderDictionary>();
        barracksInv = GetComponent<LocalStorageDictionary>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other != null)
        {
            worker = other.GetComponent<Worker>();

            if (other.gameObject.tag.Equals("Worker") && other.GetComponent<Worker>().state == State.DELIVERING_TO_BARRACKS)
            {
                worker.DropInStorage(gameObject.GetComponent<LocalStorageDictionary>());
            }
        }
    }

    public void MakeOrder(int logsNeeded,  GameObject unit)
    {
        List<GameObject> order = new List<GameObject>();
        for (int i = 0; i < logsNeeded; i++)
        {
            order.Add(logs);
        }
        order.Add(unit);
        
        Debug.Log("Unit: " + unit + " | " + "Logs needed: " + logsNeeded);

        orders.Add(order);
    }

    public void CheckIfUnitCanBeMade()
    {
        foreach (var order in orders.GetTable())
        {
            //order.Value.
        }
    }
}
