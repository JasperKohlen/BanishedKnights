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
    private void OnTriggerStay(Collider other)
    {
        if (other != null)
        {
            worker = other.GetComponent<Worker>();

            if (other.gameObject.tag.Equals("Worker") && other.GetComponent<Worker>().state == State.DELIVERING_TO_STORAGE)
            {
                worker.DropInStorage(gameObject.GetComponent<LocalStorageDictionary>());
            }
            if (other.gameObject.tag.Equals("Worker") && other.GetComponent<Worker>().state == State.COLLECTING_TO_BUILD)
            {
                worker.CollectResource(gameObject.GetComponent<LocalStorageDictionary>());
            }
        }
    }
}
