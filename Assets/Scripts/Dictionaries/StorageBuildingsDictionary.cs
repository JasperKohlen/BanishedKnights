using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageBuildingsDictionary : MonoBehaviour
{
    private Dictionary<int, GameObject> storageHouses = new Dictionary<int, GameObject>();

    public void Add(GameObject go)
    {
        int id = go.GetInstanceID();

        if (!storageHouses.ContainsKey(id))
        {
            storageHouses.Add(id, go);
        }
    }
    public void RemoveFromTable(GameObject go)
    {
        int id = go.GetInstanceID();
        if (storageHouses.ContainsKey(id))
        {
            storageHouses.Remove(id);
        }
    }
    public Dictionary<int, GameObject> GetTable()
    {
        return storageHouses;
    }
}