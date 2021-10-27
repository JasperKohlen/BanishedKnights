using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class HarvestResourceAction : GoapAction
{
    private bool isHarvested = false;
    private SelectedDictionary selectedTable;
    private ToBuildDictionary structsToBuild;
    private HarvestableComponent targetHarvest;
    private void Start()
    {
        selectedTable = EventSystem.current.GetComponent<SelectedDictionary>();
        structsToBuild = EventSystem.current.GetComponent<ToBuildDictionary>();
    }
    public HarvestResourceAction()
    {
        addPrecondition("structuresToBuild", true);
        addPrecondition("holdingResource", false);
        addEffect("holdingResource", true);
    }
    public override bool checkProceduralPrecondition(GameObject agent)
    {
        HarvestableComponent closest = null;
        float closestDist = 0f;
        IEnumerable<HarvestableComponent> harvestables = null;
        if (structsToBuild.GetTable().Count > 0)
        {
            List<GameObject> sortedStructs = new List<GameObject>();
            foreach (var item in structsToBuild.GetTable())
            {
                sortedStructs.Add(item.Value);
            }
            sortedStructs = sortedStructs.OrderBy(s => Vector3.Distance(gameObject.transform.position, s.transform.position)).ToList();
            agent.GetComponent<WorkerScript>().structToDeliverTo = sortedStructs.First();
        }
        else
        {
            agent.GetComponent<WorkerScript>().structToDeliverTo = null;
        }

        if (agent.GetComponent<WorkerScript>().structToDeliverTo == null) return false;
        if (structsToBuild.GetTable().Count <= 0) return false;

        //Algorithm to find the nearest needed resource
        #region algorithm
        if (agent.GetComponent<WorkerScript>().structToDeliverTo.GetComponent<StructureBuild>().AllLogsOrdered()
            && agent.GetComponent<WorkerScript>().structToDeliverTo.GetComponent<StructureBuild>().AllCobblesOrdered()) return false;

        if (agent.GetComponent<WorkerScript>().structToDeliverTo.GetComponent<StructureBuild>().AllLogsOrdered() == false)
        {
            harvestables = FindObjectsOfType<HarvestableComponent>().Where(s => s.resource.GetComponent<LogComponent>());
            agent.GetComponent<WorkerScript>().structToDeliverTo.GetComponent<StructureBuild>().OrderLogs();
        }
        else if (agent.GetComponent<WorkerScript>().structToDeliverTo.GetComponent<StructureBuild>().AllCobblesOrdered() == false)
        {
            harvestables = FindObjectsOfType<HarvestableComponent>().Where(s => s.resource.GetComponent<CobbleComponent>());
            agent.GetComponent<WorkerScript>().structToDeliverTo.GetComponent<StructureBuild>().OrderCobbles();
        }
        if (harvestables != null)
        {
            foreach (var harvestable in harvestables)
            {
                if (!harvestable.isTarget)
                {
                    if (closest == null)
                    {
                        // first one, so choose it for now
                        closest = harvestable;
                        closestDist = (harvestable.gameObject.transform.position - agent.transform.position).magnitude;
                    }
                    else
                    {
                        // is this one closer than the last?
                        float dist = (harvestable.gameObject.transform.position - agent.transform.position).magnitude;
                        if (dist < closestDist)
                        {
                            //found a closer one, choose it
                            closest = harvestable.GetComponent<HarvestableComponent>();
                            closestDist = dist;
                        }
                    }
                }
            }
        }
        #endregion algorithm end

        if (closest == null) return false;

        targetHarvest = closest;
        targetHarvest.isTarget = true;
        target = targetHarvest.gameObject;
        targetHarvest.gameObject.AddComponent<SelectionComponent>();

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
