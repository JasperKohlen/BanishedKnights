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
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Worker") && other.GetComponent<Worker>().state == State.DELIVERING_TO_STORAGE)
        {
            worker = other.GetComponent<Worker>();
            worker.DropInStorage(gameObject.GetComponent<LocalStorageDictionary>());
        }
    }
}