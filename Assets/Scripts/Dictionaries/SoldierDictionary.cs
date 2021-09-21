using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierDictionary : MonoBehaviour, IDictionary
{
    private Dictionary<int, GameObject> soldiers = new Dictionary<int, GameObject>();

    public void Add(GameObject go)
    {
        int id = go.GetInstanceID();

        if (!soldiers.ContainsKey(id))
        {
            soldiers.Add(id, go);
        }
    }
    public void Remove(GameObject go)
    {
        int id = go.GetInstanceID();
        if (soldiers.ContainsKey(id))
        {
            soldiers.Remove(id);
        }
    }
    public Dictionary<int, GameObject> GetTable()
    {
        return soldiers;
    }
}
