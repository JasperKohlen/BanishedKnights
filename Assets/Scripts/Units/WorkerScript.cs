using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class WorkerScript : Labourer
{
    private string neededResource;
    private GameObject resourceToHarvest;
    [HideInInspector] public GameObject structToDeliverTo;

    public override HashSet<KeyValuePair<string, object>> createGoalState()
    {
        HashSet<KeyValuePair<string, object>> goal = new HashSet<KeyValuePair<string, object>>();
        if (structsToBuild.GetTable().Count <= 0)
        {
            goal.Add(new KeyValuePair<string, object>("deliverToStorage", true));
        }
        else
        {
            goal.Add(new KeyValuePair<string, object>("deliverToBuild", true));
        }
        return goal;
    }

    public void CarryResource(GameObject resource)
    {
        //Carry resource
        resource.GetComponent<Rigidbody>().useGravity = false;
        resource.GetComponent<Rigidbody>().isKinematic = true;
        resource.transform.position = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y + 3, this.gameObject.transform.position.z);
        resource.transform.SetParent(this.gameObject.transform, true);

        //Place in worker inventory 
        inv.Add(resource);
    }

    public void DropResource(GameObject resource)
    {
        resource.transform.parent = null;
        resource.transform.position = gameObject.transform.position;
        resource.GetComponent<BoxCollider>().enabled = false;

        inv.Remove(resource);
    }

    public bool FindStorageToCollectFrom(out GameObject target)
    {
        foreach (var storage in storages.GetTable())
        {
            foreach (var toBuild in structsToBuild.GetTable())
            {
                //If a buildable requires logs and a storage house has atleast one log
                if (!toBuild.Value.GetComponent<StructureBuild>().AllLogsDelivered() && storage.Value.GetComponent<LocalStorageDictionary>().GetLogsCount() > 0)
                {
                    neededResource = "Logs";
                    storage.Value.GetComponent<StorageController>().AddToPickups(neededResource);

                    if (storage.Value.GetComponent<StorageController>().NoMoreNeededOfResource(neededResource))
                    {
                        target = null;
                        return false;
                    }

                    target = storage.Value.transform.gameObject;
                    return true;
                }
                //If a buildable requires cobbles and a storage house has atleast one cobble
                if (!toBuild.Value.GetComponent<StructureBuild>().AllCobblesDelivered() && storage.Value.GetComponent<LocalStorageDictionary>().GetCobblesCount() > 0)
                {
                    neededResource = "Cobbles";
                    storage.Value.GetComponent<StorageController>().AddToPickups(neededResource);

                    if (storage.Value.GetComponent<StorageController>().NoMoreNeededOfResource(neededResource))
                    {
                        target = null;
                        return false;
                    }

                    target = storage.Value.transform.gameObject;
                    return true;
                }
            }
        }
        target = null;
        return false;
    }

    public string GetNeededResource()
    {
        return neededResource;
    }

    public GameObject GetResourceToHarvest()
    {
        return resourceToHarvest;
    }

    public void SetResourceToHarvest(HarvestableComponent harvestable)
    {
        resourceToHarvest = harvestable.resource;
    }
}
