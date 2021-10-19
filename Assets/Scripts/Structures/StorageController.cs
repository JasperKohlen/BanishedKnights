using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StorageController : MonoBehaviour
{
    private StorageBuildingsDictionary storages;
    private LocalStorageDictionary localInv;

    private int pickupLogs;
    private int pickupCobbles;

    private void Start()
    {
        storages = EventSystem.current.GetComponent<StorageBuildingsDictionary>();
        storages.Add(gameObject);
        localInv = gameObject.GetComponent<LocalStorageDictionary>();
    }
    public bool NoMoreNeededOfResource(string resource)
    {
        if (resource == "Logs")
        {
            if (pickupLogs >= localInv.GetLogsCount())
            {
                return false;
            }
        }
        if (resource == "Cobbles")
        {
            if (pickupCobbles >= localInv.GetCobblesCount())
            {
                return false;
            }
        }
        return true;
    }

    public void AddToPickups(string resource)
    {
        if (resource == "Logs")
        {
            pickupLogs++;
        }
        if (resource == "Cobbles")
        {
            pickupCobbles++;
        }
    }

    public void ResetPickupResources()
    {
        pickupCobbles = 0;
        pickupLogs = 0;
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
