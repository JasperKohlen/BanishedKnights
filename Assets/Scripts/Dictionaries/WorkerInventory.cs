using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WorkerInventory : MonoBehaviour, IDictionary
{
    private Dictionary<int, GameObject> inventory = new Dictionary<int, GameObject>();
    private int amountOfLogs;
    private int amountOfCobbles;
    public void Add(GameObject go)
    {
        int id = go.GetInstanceID();

        if (!inventory.ContainsKey(id))
        {
            inventory.Add(id, go);
        }
    }

    public void Remove(GameObject go)
    {
        int id = go.GetInstanceID();
        if (inventory.ContainsKey(id))
        {
            inventory.Remove(id);
        }
    }

    public bool Available()
    {
        if (inventory.Count == 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public GameObject ReturnResource()
    {
        GameObject toReturn = new GameObject();
        foreach (var item in inventory.ToList())
        {
            if (item.Value.gameObject.tag.Equals("Resource"))
            {
                Debug.Log("Returning: " + item.Value.gameObject.ToString());
                toReturn = item.Value.gameObject;
            }
        }
        return toReturn;
    }
    public bool HoldingResource()
    {
        if (inventory.Any(item => item.Value.CompareTag("Resource")))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public bool HoldingLogs()
    {
        if (inventory.Any(item => item.Value.GetComponent<LogComponent>()))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public bool HoldingCobbles()
    {
        if (inventory.Any(item => item.Value.GetComponent<CobbleComponent>()))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public Dictionary<int, GameObject> GetTable()
    {
        return inventory;
    }
}
