using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WorkerInventory : MonoBehaviour
{
    private Dictionary<int, GameObject> inventory = new Dictionary<int, GameObject>();
    private int amountOfLogs;
    private int amountOfCobbles;
    public void Add(GameObject go)
    {
        int id = go.GetInstanceID();

        if (!inventory.ContainsKey(id))
        {
            if (go.name.Contains("Logs"))
            {
                amountOfLogs++;
            }
            if (go.name.Contains("Cobbles"))
            {
                amountOfCobbles++;
            }
            inventory.Add(id, go);
        }
    }

    public void RemoveFromTable(GameObject go)
    {
        int id = go.GetInstanceID();
        if (inventory.ContainsKey(id))
        {
            if (go.name.Contains("Logs"))
            {
                amountOfLogs--;
            }
            if (go.name.Contains("Cobbles"))
            {
                amountOfCobbles--;
            }
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

    //REFACTOR NEEDED IN FUTURE
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
    public bool CarryingLogs()
    {
        if (amountOfLogs > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public bool CarryingCobble()
    {
        if (amountOfCobbles > 0)
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
