using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LocalStorageDictionary : MonoBehaviour
{
    private Dictionary<int, GameObject> resourcesInLocalStorage = new Dictionary<int, GameObject>();
    private UIManager ui;
    private int logs;
    private int cobbles;
    private void Start()
    {
        ui = GameObject.FindObjectOfType<UIManager>();
    }
    public void Add(GameObject go)
    {
        int id = go.GetInstanceID();

        if (!resourcesInLocalStorage.ContainsKey(id))
        {
            if (go.name.Contains("Logs"))
            {
                logs++;
            }
            if (go.name.Contains("Cobbles"))
            {
                cobbles++;
            }
            resourcesInLocalStorage.Add(id, go);
            ui.UpdateStorage(this);
        }
    }
    public void RemoveFromStorage(GameObject go)
    {
        int id = go.GetInstanceID();
        if (resourcesInLocalStorage.ContainsKey(id))
        {
            if (go.name.Contains("Logs"))
            {
                logs--;
            }
            if (go.name.Contains("Cobbles"))
            {
                cobbles--;
            }
            resourcesInLocalStorage.Remove(id);
            ui.UpdateStorage(this);
        }
    }
    public int GetLogsCount()
    {
        return logs;
    }
    public int GetCobblesCount()
    {
        return cobbles;
    }
    public GameObject ReturnResource(string neededResource)
    {
        GameObject toReturn = new GameObject();
        foreach (var item in resourcesInLocalStorage)
        {
            if (item.Value.gameObject.name.Contains(neededResource))
            {
                Debug.Log("Returning: " + item.Value.gameObject.ToString());
                toReturn = item.Value.gameObject;
            }
        }
        return toReturn;
    }
    public Dictionary<int, GameObject> GetTable()
    {
        return resourcesInLocalStorage;
    }
}
