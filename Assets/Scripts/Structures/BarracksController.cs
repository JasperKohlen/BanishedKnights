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

    public void MakeOrder(GameObject unit)
    {
        orders.Add(unit);
        Debug.Log("Ordered a " + unit + " | " + "Logs needed: " + unit.GetComponent<Soldier>().statsSO.logsRequired);
    }

    public void CheckOrderAndTrain(GameObject worker)
    {
        //if first item in orders has required logs
        if (ItemsAvailableInStorage(orders.GetTable().First().Value.GetComponent<Soldier>().statsSO.logsRequired) && orders.GetTable().First().Value.GetComponent<Controllable>())
        {
            TrainSoldier(orders.GetTable().First().Value, worker);
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
        for (int i = 0; i < unit.GetComponent<Soldier>().statsSO.logsRequired; i++)
        {
            barracksInv.Remove(barracksInv.ReturnResource("Logs"));
        }

        orders.RemoveFirstOrder(unit);
        Debug.Log(orders.GetTable().Count + " orders");
    }
}
