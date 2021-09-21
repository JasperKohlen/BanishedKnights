using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDictionary
{
    public void Add(GameObject go);
    public void Remove(GameObject go);
    public Dictionary<int, GameObject> GetTable();

}
