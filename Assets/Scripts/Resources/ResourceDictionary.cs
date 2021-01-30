using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceDictionary : MonoBehaviour
{
    private Dictionary<int, GameObject> resourceInWorldTable = new Dictionary<int, GameObject>();

    public void AddResource(GameObject go)
    {
        int id = go.GetInstanceID();

        if (!resourceInWorldTable.ContainsKey(id))
        {
            resourceInWorldTable.Add(id, go);
        }
    }

    public void RemoveFromTable(GameObject go)
    {
        int id = go.GetInstanceID();
        resourceInWorldTable.Remove(id);
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

    public Dictionary<int, GameObject> GetTable()
    {
        return resourceInWorldTable;
    }
}
