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

    public void MakeOrder(GameObject unit)
    {
        orders.Add(unit);
        Debug.Log("Ordered a " + unit + " | " + "Logs needed: " + unit.GetComponent<Soldier>().statsSO.logsRequired);
    }

    private void CompareInventoryAndOrder(GameObject worker)
    {
        foreach (var item in orders.GetTable())
        {
            if (ItemsAvailableInStorage(item.Value.GetComponent<Soldier>().statsSO.logsRequired) && item.Value.GetComponent<Controllable>())
            {
                TrainSoldier(item.Value, worker);
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
        for (int i = 0; i < unit.GetComponent<Soldier>().statsSO.logsRequired; i++)
        {
            barracksInv.Remove(barracksInv.ReturnResource("Logs"));
        }

        orders.RemoveFirstOrder(unit);
        Debug.Log(orders.GetTable().Count + " orders");
    }
}
