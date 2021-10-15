using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StorageController : MonoBehaviour
{
    private StorageBuildingsDictionary storages;
    private LocalStorageDictionary localInv;

    private Worker worker;
    private void Start()
    {
        storages = EventSystem.current.GetComponent<StorageBuildingsDictionary>();
        storages.Add(gameObject);
        localInv = gameObject.AddComponent<LocalStorageDictionary>();
    }
    public bool IsFull()
    {
        return false;
    }
    public void AddToStorage(GameObject resource)
    {
        localInv.Add(resource);
    }
}
