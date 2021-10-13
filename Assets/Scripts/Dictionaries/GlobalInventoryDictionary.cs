using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalInventoryDictionary : MonoBehaviour
{
    private Dictionary<int, GameObject> inventory = new Dictionary<int, GameObject>();

    public void Add(GameObject go)
    {
        int id = go.GetInstanceID();

        if (!inventory.ContainsKey(id))
        {
            inventory.Add(id, go);
            go.AddComponent<SelectionComponent>();
        }
    }
    public void RemoveAll()
    {
        foreach (var item in inventory)
        {
            inventory.Remove(item.Key);
        }
    }
    public Dictionary<int, GameObject> GetTable()
    {
        return inventory;
    }
}
