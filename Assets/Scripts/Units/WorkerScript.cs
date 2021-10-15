using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerScript : Labourer
{
    public override HashSet<KeyValuePair<string, object>> createGoalState()
    {
        HashSet<KeyValuePair<string, object>> goal = new HashSet<KeyValuePair<string, object>>();

        goal.Add(new KeyValuePair<string, object>("collectResources", true));

        return goal;
    }

    public void CarryResource(GameObject resource)
    {
        //Carry resource ingame
        resource.GetComponent<Rigidbody>().useGravity = false;
        resource.GetComponent<Rigidbody>().isKinematic = true;
        resource.transform.position = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y + 3, this.gameObject.transform.position.z);
        resource.transform.SetParent(this.gameObject.transform, true);

        //Place in worker inventory 
        inv.Add(resource);
    }

    public GameObject ReturnResourceInHands()
    {
        return inv.ReturnResource();
    }

    public void DropResource(GameObject resource)
    {
        resource.transform.parent = null;
        resource.transform.position = gameObject.transform.position;
        resource.GetComponent<BoxCollider>().enabled = false;

        inv.Remove(resource);
    }
}
