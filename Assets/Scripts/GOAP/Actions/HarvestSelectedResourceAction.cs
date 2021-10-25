using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class HarvestSelectedResourceAction : GoapAction
{
    private bool isHarvested = false;
    private SelectedDictionary selectedTable;
    private HarvestableComponent targetHarvest;
    private void Start()
    {
        selectedTable = EventSystem.current.GetComponent<SelectedDictionary>();
    }
    public HarvestSelectedResourceAction()
    {
        addPrecondition("resourcesSelected", true);
        addPrecondition("holdingResource", false);
        addEffect("holdingResource", true);
        addEffect("toStorage", true);
    }
    public override bool checkProceduralPrecondition(GameObject agent)
    {
        HarvestableComponent closest = null;
        float closestDist = 0f;

        foreach (var harvestable in selectedTable.GetTable())
        {
            if (!harvestable.Value.GetComponent<HarvestableComponent>().isTarget)
            {
                if (closest == null)
                {
                    // first one, so choose it for now
                    closest = harvestable.Value.GetComponent<HarvestableComponent>();
                    closestDist = (harvestable.Value.gameObject.transform.position - agent.transform.position).magnitude;
                }
                else
                {
                    // is this one closer than the last?
                    float dist = (harvestable.Value.gameObject.transform.position - agent.transform.position).magnitude;
                    if (dist < closestDist)
                    {
                        //found a closer one, choose it
                        closest = harvestable.Value.GetComponent<HarvestableComponent>();
                        closestDist = dist;
                    }
                }
            }
        }

        if (closest == null) return false;

        targetHarvest = closest;
        targetHarvest.isTarget = true;
        target = targetHarvest.gameObject;
        agent.GetComponent<WorkerScript>().SetResourceToHarvest(targetHarvest);

        return closest != null;
    }

    public override bool isDone()
    {
        return isHarvested;
    }

    public override bool perform(GameObject agent)
    {
        if (!targetHarvest.harvested && targetHarvest.isTarget)
        {
            WorkerScript worker = agent.GetComponent<WorkerScript>();
            ResourceDropper harvestable = targetHarvest.GetComponent<ResourceDropper>();
            GameObject resource = Instantiate(targetHarvest.resource);

            //Deselect from selected resources
            selectedTable.Remove(targetHarvest.gameObject);
            //Destroy harvestable
            harvestable.Break(targetHarvest);

            //Carry resource ingame
            worker.CarryResource(resource);
            isHarvested = true;

            return true;
        }
        return false;
    }

    public override bool requiresInRange()
    {
        return true;
    }

    public override void reset()
    {
        isHarvested = false;
        targetHarvest = null;
    }
}
