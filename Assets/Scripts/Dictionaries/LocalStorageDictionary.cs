using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalStorageDictionary : MonoBehaviour
{
    private Dictionary<int, GameObject> resourcesInLocalStorage = new Dictionary<int, GameObject>();
    private UIManager ui;
    private void Start()
    {
        ui = GameObject.Find("Canvas").GetComponent<UIManager>();
    }
    public void Add(GameObject go)
    {
        int id = go.GetInstanceID();

        if (!resourcesInLocalStorage.ContainsKey(id))
        {
            resourcesInLocalStorage.Add(id, go);
            ui.UpdateStorage(this);
        }
    }
    public Dictionary<int, GameObject> GetTable()
    {
        return resourcesInLocalStorage;
    }
}
