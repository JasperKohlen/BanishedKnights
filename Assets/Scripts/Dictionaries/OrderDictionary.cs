using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class OrderDictionary : MonoBehaviour
{
    private Dictionary<int, List<GameObject>> orders = new Dictionary<int, List<GameObject>>();
    public void Add(List<GameObject> order)
    {
        int id = order.First().GetInstanceID();

        if (!orders.ContainsKey(id))
        {
            orders.Add(id, order);
        }
    }
    public void Remove(List<GameObject> order)
    {
        //int id = order.Any(s => s.GetInstanceID());
        //if (orders.ContainsKey(id))
        //{
        //    //if (go.name.Contains("Logs"))
        //    //{
        //    //    logs--;
        //    //}
        //    //if (go.name.Contains("Cobbles"))
        //    //{
        //    //    cobbles--;
        //    //}
        //    orders.Remove(id);
        //}
    }
    public Dictionary<int, List<GameObject>> GetTable()
    {
        return orders;
    }
}
