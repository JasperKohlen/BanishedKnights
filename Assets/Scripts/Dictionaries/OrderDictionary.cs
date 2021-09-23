using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class OrderDictionary : MonoBehaviour
{
    private Dictionary<int, List<GameObject>> orders = new Dictionary<int, List<GameObject>>();
    private UIManager ui;
    private void Start()
    {
        ui = FindObjectOfType<UIManager>();
    }
    public void Add(List<GameObject> order)
    {
        int id = order.GetHashCode();

        if (!orders.ContainsKey(id))
        {
            Debug.Log("Order");
            orders.Add(id, order);
            ui.UpdateBarracksMenu(this);
        }
    }
    public void Remove(List<GameObject> order)
    {
        int id = order.GetHashCode();
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
            List<GameObject> orderToRemove = orders.First().Value;
            Remove(orderToRemove);
            ui.UpdateBarracksMenu(this);
        }
    }

    public int LogsNeeded()
    {
        int logs = 0;
        foreach (var order in orders)
        {
            foreach (var item in order.Value)
            {
                if (item.name.Contains("Logs"))
                {
                    logs++;
                }
            }
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
    public Dictionary<int, List<GameObject>> GetTable()
    {
        return orders;
    }
}
