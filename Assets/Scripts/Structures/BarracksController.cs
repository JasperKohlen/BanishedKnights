using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class BarracksController : MonoBehaviour
{
    [SerializeField] private GameObject logs;

    private Worker worker;
    private OrderDictionary orders;
    private LocalStorageDictionary barracksInv;
    private void Start()
    {
        orders = GetComponent<OrderDictionary>();
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
                CompareInventoryAndOrder(other.gameObject);
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
        
        Debug.Log("Ordered a " + unit + " | " + "Logs needed: " + logsNeeded);

        orders.Add(order);
    }

    private void CompareInventoryAndOrder(GameObject worker)
    {
        int logs = 0;
        foreach (var item in orders.GetTable().First().Value)
        {
            if (item.name.Contains("Logs"))
            {
                logs++;
            }
            if (ItemsAvailableInStorage(logs) && item.GetComponent<Controllable>())
            {
                TrainSoldier(item, worker);
            }
        }
    }

    private bool ItemsAvailableInStorage(int logs)
    {
        if (barracksInv.GetLogsCount() >= logs)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void TrainSoldier(GameObject unit, GameObject worker)
    {
        Destroy(worker);
        Instantiate(unit, worker.transform.position, Quaternion.identity);

        foreach (var item in orders.GetTable().First().Value)
        {
            if (item.name.Contains("Logs"))
            {
                barracksInv.Remove(barracksInv.ReturnResource(item.name));
            }
        }

        orders.Remove(orders.GetTable().First().Value);
        Debug.Log(orders.GetTable().Count + " orders");
    }
}
