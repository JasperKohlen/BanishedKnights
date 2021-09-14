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
    // Start is called before the first frame update
    void Start()
    {
        structsToBuild = EventSystem.current.GetComponent<ToBuildDictionary>();
        structsToBuild.AddStruct(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Worker") && other.gameObject.GetComponent<Worker>().inventory.HoldingResource())
        {
            Debug.Log("Materials delivered: ");
            worker = other.gameObject.GetComponent<Worker>();

            if (worker.resourceToDeliver.Get().name.Contains("Logs"))
            {
                deliveredLogs++;
            }
            if (worker.resourceToDeliver.Get().name.Contains("Cobbles"))
            {
                deliveredcobbles++;
            }

            deliveredObjects.Add(worker.resourceToDeliver.Get());

            CheckDeliveredResources();
            worker.PlaceResourceToBuild();
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

    void CompleteBuilding()
    {
        ConsumeResources();
        structsToBuild.RemoveFromTable(gameObject);

        Instantiate(prefab.Structure_to_build, prefab.position, Quaternion.identity);
        Destroy(this.gameObject);
    }

    void ConsumeResources()
    {
        foreach (var item in deliveredObjects)
        {
            Destroy(item);
        }
    }
}
