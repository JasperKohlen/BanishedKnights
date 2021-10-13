using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class OrderDictionary : MonoBehaviour
{
    private Dictionary<int, GameObject> orders = new Dictionary<int,GameObject>();
    private UIBarracksManager ui;

    private int swordmen = 0;
    private int bowmen = 0;
    private void Start()
    {
        ui = FindObjectOfType<UIBarracksManager>();
    }
    public void Add(GameObject unit)
    {
        int id = GenerateRandomID(unit);

        if (!orders.ContainsKey(id))
        {
            orders.Add(id, unit);
        }
        CountIndividualSoldiers();
        ui.UpdateBarracksMenu(this);
    }
    private void Remove(GameObject unit)
    {
        //Foreach is utilized because of a bug with the unit id, therefore i check if the object is the same
        foreach (var item in orders)
        {
            if (item.Value == unit)
            {
                orders.Remove(item.Key);
                break;
            }
        }
    }

    public void RemoveFirstOrder(GameObject unit)
    {
        if (orders.Count != 0)
        {
            GameObject orderToRemove = null;

            if(unit.GetComponent<SwordmanComponent>())
            {
                orderToRemove = orders.Where(s => s.Value.GetComponent<SwordmanComponent>()).First().Value;
            }
            if (unit.GetComponent<BowmanComponent>())
            {
                orderToRemove = orders.Where(s => s.Value.GetComponent<BowmanComponent>()).First().Value;
            }
            if (orderToRemove != null)
            {
                Remove(orderToRemove);
                CountIndividualSoldiers();
                ui.UpdateBarracksMenu(this);
            }
        }
    }

    public int LogsNeeded()
    {
        int logs = 0;
        foreach (var unit in orders)
        {
            logs += unit.Value.GetComponent<Soldier>().statsSO.logsRequired;
        }
        return logs;
    }

    public bool Available()
    {
        if (orders.Count > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private int GenerateRandomID(GameObject unit)
    {
        int id = Random.Range(0, 10000);

        if (orders.Any(s => s.Value.GetComponent<Soldier>().statsSO.id.Equals(id)))
        {
            GenerateRandomID(unit);
        }
        unit.GetComponent<Soldier>().statsSO.id = id;
        return id;
    }
    private void CountIndividualSoldiers()
    {
        swordmen = 0;
        bowmen = 0;

        foreach (var item in orders)
        {

            if (item.Value.GetComponent<SwordmanComponent>())
            {
                swordmen += 1;
            }
            if (item.Value.GetComponent<BowmanComponent>())
            {
                bowmen += 1;
            }
        }
    }

    public int GetSwordmanCount()
    {
        return swordmen;
    }

    public int GetBowmanCount()
    {
        return bowmen;
    }
    public Dictionary<int, GameObject> GetTable()
    {
        return orders;
    }
}
