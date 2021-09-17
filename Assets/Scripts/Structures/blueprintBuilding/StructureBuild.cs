using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class StructureBuild : MonoBehaviour
{
    private List<GameObject> deliveredObjects = new List<GameObject>();

    [Header("Scriptable Object")]
    [SerializeField] private BlueprintSO prefab;

    private Worker worker;

    private int deliveredLogs;
    private int deliveredcobbles;

    private ToBuildDictionary structsToBuild;

    private Vector3 position_To_Place;
    private Quaternion rotation_To_Place;
    // Start is called before the first frame update
    void Start()
    {
        structsToBuild = EventSystem.current.GetComponent<ToBuildDictionary>();
        structsToBuild.AddStruct(gameObject);

        position_To_Place = prefab.position;
        rotation_To_Place = prefab.rotation;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Worker") && 
            other.gameObject.GetComponent<Worker>().inventory.HoldingResource() && 
            other.GetComponent<Worker>().state == State.DELIVERING_TO_BUILD)
        {
            worker = other.gameObject.GetComponent<Worker>();

            if (worker.inventory.ReturnResource().name.Contains("Logs") && !AllLogsDelivered())
            {
                Debug.Log("Logs delivered: ");
                deliveredObjects.Add(worker.resourceToDeliver.Get());
                worker.PlaceResourceToBuild();
                deliveredLogs++;
            }
            if (worker.inventory.ReturnResource().name.Contains("Cobbles") && !AllCobblesDelivered())
            {
                Debug.Log("Cobbles delivered: ");
                deliveredObjects.Add(worker.resourceToDeliver.Get());
                worker.PlaceResourceToBuild();
                deliveredcobbles++;
            }

            CheckDeliveredResources();
        }
    }

    void CheckDeliveredResources()
    {
        //Debug.Log("Logs delivered " + deliveredLogs);
        //Debug.Log("Logs needed " + logsNeeded);
        //Debug.Log("cobbles delivered " + deliveredcobbles);
        //Debug.Log("cobbles needed " + cobblesNeeded);
        if (deliveredLogs >= prefab.logsNeeded && deliveredcobbles >= prefab.cobblesNeeded)
        {
            CompleteBuilding();
        }
    }

    public bool IsUnfinished()
    {
        if (!AllLogsDelivered() || !AllCobblesDelivered())
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool AllLogsDelivered()
    {
        if (deliveredLogs >= prefab.logsNeeded)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool AllCobblesDelivered()
    {
        if (deliveredcobbles >= prefab.cobblesNeeded)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void CompleteBuilding()
    {
        ConsumeResources();
        structsToBuild.RemoveFromTable(gameObject);

        Instantiate(prefab.Resulting_Building, position_To_Place, rotation_To_Place);
        Destroy(gameObject);
    }

    void ConsumeResources()
    {
        foreach (var item in deliveredObjects)
        {
            Destroy(item);
        }
    }
}
