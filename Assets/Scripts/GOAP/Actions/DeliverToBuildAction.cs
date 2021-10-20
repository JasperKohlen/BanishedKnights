using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class DeliverToBuildAction : GoapAction
{
    private bool delivered;
    private ToBuildDictionary structs;
    private GameObject targetStruct;
    private void Start()
    {
        structs = EventSystem.current.GetComponent<ToBuildDictionary>();
    }
    public DeliverToBuildAction()
    {
        addPrecondition("holdingResource", true);
        addPrecondition("structuresToBuild", true);
        addEffect("holdingResource", false);
        addEffect("deliverToBuild", true);
    }
    public override bool checkProceduralPrecondition(GameObject agent)
    {
        GameObject structToDeliverTo;
        if (agent.GetComponent<WorkerScript>().structToDeliverTo == null) return false;

        structToDeliverTo = agent.GetComponent<WorkerScript>().structToDeliverTo;

        if (structToDeliverTo == null) return false;

        targetStruct = structToDeliverTo;
        target = targetStruct;

        return targetStruct != null;
    }

    public override bool isDone()
    {
        return delivered;
    }

    public override bool perform(GameObject agent)
    {
        WorkerScript worker = agent.GetComponent<WorkerScript>();
        targetStruct.GetComponent<StructureBuild>().AddToStructure(worker.inv.ReturnResource());
        worker.DropResource(worker.inv.ReturnResource());
        delivered = true;
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
