using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalStorageDictionary : MonoBehaviour
{
    private Dictionary<int, GameObject> resourcesInLocalStorage = new Dictionary<int, GameObject>();

    public void Add(GameObject go)
    {
        int id = go.GetInstanceID();

        if (!resourcesInLocalStorage.ContainsKey(id))
        {
            resourcesInLocalStorage.Add(id, go);
        }
    }
    public Dictionary<int, GameObject> GetTable()
    {
        return resourcesInLocalStorage;
    }
}
