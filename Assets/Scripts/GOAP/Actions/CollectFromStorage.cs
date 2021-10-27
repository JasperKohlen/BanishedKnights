using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class CollectFromStorage : GoapAction
{
    private bool isCollected = false;
    private StorageController targetStorage;

    public CollectFromStorage()
    {
        addPrecondition("unitsOrdered", true);
        addPrecondition("holdingResource", false);
        addEffect("holdingResource", true);
        addEffect("toBarracks", true);
    }
    public override bool checkProceduralPrecondition(GameObject agent)
    {
        if (agent.GetComponent<WorkerScript>().OrdersAvailable() == false) return false;

        agent.GetComponent<WorkerScript>().FindStorageToCollectFrom(out target);
        if (target == null) return false;
        if (agent.GetComponent<WorkerScript>().barracksToDeliverTo.GetComponent<BarracksController>().AllLogsOrdered()) return false;

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
        if (storageInv.GetLogsCount() <= 0) return false;
        
        //Collect needed resource
        GameObject resource = Instantiate(storageInv.ReturnResource("Logs"));
        storageInv.Remove(storageInv.ReturnResource("Logs"));

        //Carry resource ingame
        worker.CarryResource(resource);
        agent.GetComponent<WorkerScript>().barracksToDeliverTo.GetComponent<BarracksController>().OrderLogs();

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
