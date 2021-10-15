using System.Collections;
using System.Collections.Generic;
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
        addEffect("collectResources", true);
    }
    public override bool checkProceduralPrecondition(GameObject agent)
    {
        GameObject structToDeliverTo = null;

        foreach (var item in structs.GetTable())
        {
            if (agent.GetComponent<WorkerScript>().inv.HoldingCobbles() && !item.Value.GetComponent<StructureBuild>().AllCobblesDelivered())
            {
                structToDeliverTo = item.Value;
                break;
            }
            if (agent.GetComponent<WorkerScript>().inv.HoldingLogs() && !item.Value.GetComponent<StructureBuild>().AllLogsDelivered())
            {
                structToDeliverTo = item.Value;
                break;
            }
        }
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
        targetStruct.transform.GetComponent<StructureBuild>().AddToStructure(worker.inv.ReturnResource());
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
