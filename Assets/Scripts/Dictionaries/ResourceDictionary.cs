using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ResourceDictionary : MonoBehaviour, IDictionary
{
    private Dictionary<int, GameObject> resourceInWorldTable = new Dictionary<int, GameObject>();

    public void Add(GameObject go)
    {
        int id = go.GetInstanceID();

        if (!resourceInWorldTable.ContainsKey(id))
        {
            resourceInWorldTable.Add(id, go);
        }
    }

    public void Remove(GameObject go)
    {
        int id = go.GetInstanceID();
        if (resourceInWorldTable.ContainsKey(id))
        {
            resourceInWorldTable.Remove(id);
        }
    }

    public bool Available()
    {
        if (resourceInWorldTable.Count == 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public GameObject Get()
    {
        return resourceInWorldTable.ToList().First().Value.gameObject;
    }

    public Dictionary<int, GameObject> GetTable()
    {
        return resourceInWorldTable;
    }
}
