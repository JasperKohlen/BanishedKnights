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
    public BlueprintSO so;

    private int deliveredLogs;
    private int deliveredcobbles;

    private ToBuildDictionary structsToBuild;
    private SelectedDictionary resources;

    private Vector3 position_To_Place;
    private Quaternion rotation_To_Place;
    // Start is called before the first frame update
    void Start()
    {
        resources = EventSystem.current.GetComponent<SelectedDictionary>();

        structsToBuild = EventSystem.current.GetComponent<ToBuildDictionary>();
        structsToBuild.Add(gameObject);

        position_To_Place = so.position;
        rotation_To_Place = so.rotation;
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag.Equals("Removable"))
        {
            resources.Add(other.gameObject);
        }
    }
    public void AddToStructure(GameObject resource)
    {
        if (resource.GetComponent<LogComponent>())
        {
            deliveredLogs++;
        }
        if (resource.GetComponent<CobbleComponent>())
        {
            deliveredcobbles++;
        }

        deliveredObjects.Add(resource);
        CheckDeliveredResources();
    }
    void CheckDeliveredResources()
    {
        if (deliveredLogs >= so.logsNeeded && deliveredcobbles >= so.cobblesNeeded)
        {
            CompleteBuilding();
        }
    }

    public bool AllLogsDelivered()
    {
        if (deliveredLogs >= so.logsNeeded)
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
        if (deliveredcobbles >= so.cobblesNeeded)
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
        structsToBuild.Remove(gameObject);

        Instantiate(so.Resulting_Building, position_To_Place, rotation_To_Place);
        Destroy(gameObject);
    }

    void ConsumeResources()
    {
        foreach (var item in deliveredObjects)
        {
            Destroy(item);
        }
    }

    public float GetPctComplete()
    {
        int totalNeeded = so.logsNeeded + so.cobblesNeeded;
        int totalDelivered = deliveredLogs + deliveredcobbles;

        float pct;
        pct = (float)totalDelivered / (float)totalNeeded;
        return pct;
    }
}
