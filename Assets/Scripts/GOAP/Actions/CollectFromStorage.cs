using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class CollectFromStorage : GoapAction
{
    private StorageBuildingsDictionary storages;
    private bool isCollected = false;
    private StorageController targetStorage;
    private GameObject neededResource;
    private void Start()
    {
        storages = EventSystem.current.GetComponent<StorageBuildingsDictionary>();
    }
    public CollectFromStorage()
    {
        //Precondition: Storage that contains required item
        addPrecondition("structuresToBuild", true);
        addPrecondition("holdingResource", false);
        addEffect("holdingResource", true);
    }
    public override bool checkProceduralPrecondition(GameObject agent)
    {
        List<GameObject> sortedStorages = new List<GameObject>();
        foreach (var item in storages.GetTable())
        {
            if (!item.Value.GetComponent<StorageController>().IsFull())
            {
                sortedStorages.Add(item.Value);
            }
        }
        sortedStorages = sortedStorages.OrderBy(s => Vector3.Distance(gameObject.transform.position, s.transform.position)).ToList();
        targetStorage = sortedStorages.First().GetComponent<StorageController>();
        target = targetStorage.gameObject;

        return targetStorage != null;
    }

    public override bool isDone()
    {
        return isCollected;
    }

    public override bool perform(GameObject agent)
    {
        WorkerScript worker = agent.GetComponent<WorkerScript>();
        LocalStorageDictionary storageInv = targetStorage.gameObject.GetComponent<LocalStorageDictionary>();
        //Improve this line obviously
        GameObject resource = Instantiate(storageInv.ReturnResource("Logs"));
        storageInv.Remove(storageInv.ReturnResource("Logs"));

        //Carry resource ingame
        worker.CarryResource(resource);

        isCollected = true;

        return true;
    }

    public override bool requiresInRange()
    {
        return true;
    }

    public override void reset()
    {
        isCollected = false;
        targetStorage = null;
    }
}
