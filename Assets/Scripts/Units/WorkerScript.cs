using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class WorkerScript : Labourer
{
    [HideInInspector] public GameObject structToDeliverTo = null;
    [HideInInspector] public GameObject barracksToDeliverTo = null;

    public override HashSet<KeyValuePair<string, object>> createGoalState()
    {
        HashSet<KeyValuePair<string, object>> goal = new HashSet<KeyValuePair<string, object>>();
        goal.Add(new KeyValuePair<string, object>("idle", true));

        if (OrdersAvailable() && FindObjectsOfType<LocalStorageDictionary>().Any(s => s.GetLogsCount() > 0))
        {
            goal.Clear();
            goal.Add(new KeyValuePair<string, object>("deliverToBarracks", true));
        }
        if (selected.GetTable().Count > 0)
        {
            goal.Clear();
            goal.Add(new KeyValuePair<string, object>("deliverToStorage", true));
        }
        if (structsToBuild.GetTable().Count > 0)
        {
            goal.Clear();
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
        barracksToDeliverTo = FindBarracksToDeliverTo();
        foreach (var storage in storages.GetTable())
        {
            if (storage.Value.GetComponent<LocalStorageDictionary>().GetLogsCount() > 0)
            {
                target = storage.Value.transform.gameObject;
                return true;
            }
        }
        target = null;
        return false;
    }

    public GameObject FindBarracksToDeliverTo()
    {
        List<GameObject> barracksWithOrders = new List<GameObject>();
        foreach (var order in FindObjectsOfType<OrderDictionary>())
        {
            if (order.Available())
            {
                barracksWithOrders.Add(order.gameObject);
            }
        }

        barracksWithOrders = barracksWithOrders.OrderBy(s => Vector3.Distance(gameObject.transform.position, s.transform.position)).ToList();
        return barracksWithOrders.First();
    }
}
