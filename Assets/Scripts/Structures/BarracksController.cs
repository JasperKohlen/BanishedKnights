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
    private int orderedLogs = 0;
    private OrderDictionary orders;
    private LocalStorageDictionary barracksInv;
    [SerializeField] private UIBarracksManager ui;
    private void Start()
    {
        orders = GetComponent<OrderDictionary>();
        barracksInv = GetComponent<LocalStorageDictionary>();
        ui = FindObjectOfType<UIBarracksManager>();
    }

    public void MakeOrder(GameObject unit)
    {
        orders.Add(unit);
        Debug.Log("Ordered a " + unit + " | " + "Logs needed: " + unit.GetComponent<Soldier>().statsSO.logsRequired);
    }

    public void CheckOrderAndTrain(GameObject worker)
    {
        ui.UpdateBarracksMenu(orders);
        //if first item in orders has required logs
        if (ItemsAvailableInBarracks(orders.GetTable().First().Value.GetComponent<Soldier>().statsSO.logsRequired) && orders.GetTable().First().Value.GetComponent<Controllable>())
        {
            TrainSoldier(orders.GetTable().First().Value, worker);
        }
    }

    public void OrderLogs()
    {
        orderedLogs++;
    }

    public bool AllLogsOrdered()
    {
        if (orderedLogs >= orders.GetTable().First().Value.GetComponent<Soldier>().statsSO.logsRequired)
        {
            return true;
        }
        return false;
    }

    private bool ItemsAvailableInBarracks(int logs)
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
        for (int i = 0; i < unit.GetComponent<Soldier>().statsSO.logsRequired; i++)
        {
            barracksInv.Remove(barracksInv.ReturnResource("Logs"));
        }

        orderedLogs = 0;
        orders.RemoveFirstOrder(unit);
        Debug.Log(orders.GetTable().Count + " orders");
    }
}
