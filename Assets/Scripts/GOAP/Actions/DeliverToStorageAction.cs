using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class DeliverToStorageAction : GoapAction
{
    private StorageBuildingsDictionary storages;
    private StorageController targetStorage;
    private bool delivered;

    private void Start()
    {
        storages = EventSystem.current.GetComponent<StorageBuildingsDictionary>();
    }
    public DeliverToStorageAction()
    {
        //addPrecondition("resourcesSelected", true);
        addPrecondition("holdingResource", true);
        addPrecondition("toStorage", true);
        addEffect("holdingResource", false);
        addEffect("deliverToStorage", true);
    }

    public override bool checkProceduralPrecondition(GameObject agent)
    {
        Debug.Log("Storaging");
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
        return delivered;
    }

    public override bool perform(GameObject agent)
    {
        targetStorage.AddToStorage(agent.GetComponent<WorkerInventory>().ReturnResource());
        agent.GetComponent<WorkerScript>().DropResource(agent.GetComponent<WorkerInventory>().ReturnResource());
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
        targetStorage = null;
    }
}
