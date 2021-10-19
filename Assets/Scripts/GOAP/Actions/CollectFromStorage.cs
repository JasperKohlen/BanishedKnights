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
        //addPrecondition: Storage that contains required item
        addPrecondition("structuresToBuild", true);
        addPrecondition("holdingResource", false);
        addEffect("holdingResource", true);
    }
    public override bool checkProceduralPrecondition(GameObject agent)
    {
        agent.GetComponent<WorkerScript>().FindStorageToCollectFrom(out target);
        if (target == null) return false;

        targetStorage = target.GetComponent<StorageController>();

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
        GameObject resource = Instantiate(storageInv.ReturnResource(worker.GetNeededResource()));
        storageInv.Remove(storageInv.ReturnResource(worker.GetNeededResource()));

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
