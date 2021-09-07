using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ResourceDropper : MonoBehaviour
{
    public GameObject logPrefab;
    private Vector3 objectPosition;
    private SelectedDictionary selectedTable;
    private Worker worker;
    void Start()
    {
        selectedTable = EventSystem.current.GetComponent<SelectedDictionary>();
        objectPosition = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 2.0f, gameObject.transform.position.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        //Only remove removable when selected and the collider is a worker who is in the removing state.
        if (other.gameObject.tag.Equals("Worker") && selectedTable.Contains(gameObject) && other.GetComponent<Worker>().state == State.REMOVING)
        {
            selectedTable.Deselect(gameObject);

            //Refactor?
            worker = other.gameObject.GetComponent<Worker>();

            GameObject log = Instantiate(logPrefab, objectPosition, Quaternion.identity);
            worker.resourcesToDeliver.AddResource(log);

            Destroy(gameObject);
        }
    }
}
