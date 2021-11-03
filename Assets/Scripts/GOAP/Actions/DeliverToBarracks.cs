using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DeliverToBarracks : GoapAction
{
    private bool delivered;
    private GameObject targetStruct;
    public DeliverToBarracks()
    {
        addPrecondition("holdingResource", true);
        addPrecondition("toBarracks", true);
        addEffect("holdingResource", false);
        addEffect("deliverToBarracks", true);
    }
    public override bool checkProceduralPrecondition(GameObject agent)
    {
        targetStruct = agent.GetComponent<WorkerScript>().barracksToDeliverTo;
        target = targetStruct;

        return targetStruct != null;
    }

    public override bool isDone()
    {
        return delivered;
    }

    public override bool perform(GameObject agent)
    {
        //if (!agent.GetComponent<WorkerScript>().OrdersAvailable())
        //{ 
        //    reset();
        //    return false;
        //}

        targetStruct.GetComponent<LocalStorageDictionary>().Add(agent.GetComponent<WorkerInventory>().ReturnResource());

        agent.GetComponent<WorkerScript>().DropResource(agent.GetComponent<WorkerInventory>().ReturnResource());
        delivered = true;
        targetStruct.GetComponent<BarracksController>().CheckOrderAndTrain(agent);
        return true;
    }

    public override bool requiresInRange()
    {
        return true;
    }

    public override void reset()
    {
        delivered = false;
        targetStruct = null;
    }
}
