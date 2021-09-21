using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToBuildDictionary : MonoBehaviour, IDictionary
{
    private Dictionary<int, GameObject> structuresToBuild = new Dictionary<int, GameObject>();

    public void Add(GameObject go)
    {
        int id = go.GetInstanceID();

        if (!structuresToBuild.ContainsKey(id))
        {
            structuresToBuild.Add(id, go);
        }
    }

    public void Remove(GameObject go)
    {
        int id = go.GetInstanceID();
        if (structuresToBuild.ContainsKey(id))
        {
            structuresToBuild.Remove(id);
        }
    }

    public bool Available()
    {
        if (structuresToBuild.Count == 0)
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
        return structuresToBuild;
    }
}
