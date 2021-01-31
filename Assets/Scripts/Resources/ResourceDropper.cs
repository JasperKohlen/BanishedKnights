using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ResourceDropper : MonoBehaviour
{
    public GameObject logPrefab;
    private bool isQuitting;
    private Vector3 objectPosition;
    private SelectedDictionary selectedTable;
    private ResourceDictionary resourcesInWorld;
    void Start()
    {
        selectedTable = EventSystem.current.GetComponent<SelectedDictionary>();
        resourcesInWorld = EventSystem.current.GetComponent<ResourceDictionary>();
        objectPosition = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 2.0f, gameObject.transform.position.z);
        isQuitting = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        //Only remove removable when selected and the collider is a worker who is in the removing state.
        if (other.gameObject.tag.Equals("Worker") && selectedTable.Contains(gameObject) && other.GetComponent<Worker>().state == Assets.Scripts.State.REMOVING)
        {
            selectedTable.Deselect(gameObject);
            Destroy(gameObject);
        }
    }
    void OnApplicationQuit()
    {
        isQuitting = true;
    }
    void OnDestroy()
    {
        //Prevent instantiation on ending playmode
        if (!isQuitting)
        {
            Instantiate(logPrefab, objectPosition, Quaternion.identity);
            resourcesInWorld.AddResource(logPrefab);
        }
    }
}
