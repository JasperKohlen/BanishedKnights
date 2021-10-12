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
        int id = unit.GetInstanceID();

        if (!orders.ContainsKey(id))
        {
            orders.Add(id, unit);
            ui.UpdateBarracksMenu(this);

            if (unit.GetComponent<SwordmanComponent>())
            {
                swordmen += 1;
            }
            if (unit.GetComponent<BowmanComponent>())
            {
                bowmen += 1;
            }
        }
    }
    public void Remove(GameObject unit)
    {
        int id = unit.GetInstanceID();
        if (orders.ContainsKey(id))
        {
            orders.Remove(id);
            ui.UpdateBarracksMenu(this);
        }
    }

    public void RemoveFirstOrder()
    {
        if (orders.Count != 0)
        {
            GameObject orderToRemove = orders.First().Value;
            Remove(orderToRemove);
            ui.UpdateBarracksMenu(this);
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
