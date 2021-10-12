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
            if (unit.GetComponent<SwordmanComponent>())
            {
                swordmen += 1;
                Debug.Log("Added swordsman order");
            }
            if (unit.GetComponent<BowmanComponent>())
            {
                bowmen += 1;
                Debug.Log("Added Bowman order");
            }
            orders.Add(id, unit);
        }
        ui.UpdateBarracksMenu(this);
    }
    public void Remove(GameObject unit)
    {
        int id = unit.GetComponent<Soldier>().statsSO.id;
        if (orders.ContainsKey(id))
        {
            if (unit.GetComponent<SwordmanComponent>())
            {
                swordmen -= 1;
                Debug.Log("Removed swordsman order");
            }
            if (unit.GetComponent<BowmanComponent>())
            {
                bowmen -= 1;
                Debug.Log("Removed Bowman order");
            }
            orders.Remove(id);
            ui.UpdateBarracksMenu(this);
        }
    }

    public void RemoveFirstOrder()
    {
        if (orders.Count != 0)
        {
            GameObject orderToRemove = orders.First().Value;

            if (orderToRemove.GetComponent<SwordmanComponent>())
            {
                swordmen -= 1;
                Debug.Log("Removed swordsman order");
            }
            if (orderToRemove.GetComponent<BowmanComponent>())
            {
                bowmen -= 1;
                Debug.Log("Removed Bowman order");
            }

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
