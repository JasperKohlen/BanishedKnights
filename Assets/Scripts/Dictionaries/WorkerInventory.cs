using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WorkerInventory : MonoBehaviour, IDictionary
{
    private Dictionary<int, GameObject> inventory = new Dictionary<int, GameObject>();
    private GameObject resource;
    private void Start()
    {
        resource = null;
    }
    public void Add(GameObject go)
    {
        int id = go.GetInstanceID();

        if (!inventory.ContainsKey(id))
        {
            inventory.Add(id, go);
        }
        if (go.tag.Equals("Resource"))
        {
            resource = go;
        }
    }

    public void Remove(GameObject go)
    {
        int id = go.GetInstanceID();
        if (inventory.ContainsKey(id))
        {
            inventory.Remove(id);
        }
        if (go.tag.Equals("Resource"))
        {
            resource = null;
        }
    }
    public GameObject ReturnResource()
    {
        return resource;
    }
    public bool HoldingResource()
    {
        if (resource != null)
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
        if (resource != null)
        {
            if (resource.GetComponent<LogComponent>())
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
    }
    public bool HoldingCobbles()
    {
        if (resource != null)
        {
            if (resource.GetComponent<CobbleComponent>())
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
    }
    public Dictionary<int, GameObject> GetTable()
    {
        return inventory;
    }
}
