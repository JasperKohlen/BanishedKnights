using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//Starts dragging blueprint once it's spawned in
public class blueprintPlacement : MonoBehaviour
{
    private RaycastHit hit;
    [SerializeField] private static blueprintPlacement instance;
    [SerializeField] private GameObject prefab;

    private StorageBuildingsDictionary storages;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Debug.Log("Instance already exists, destroying object!");
            Destroy(this);
        }
    }

    private void Start()
    {
        storages = EventSystem.current.GetComponent<StorageBuildingsDictionary>();
    }

    // Update is called once per frame
    void Update()
    {
        CarryPrefab();
        RotatePrefab();
        CarryingPrefabChecks();

        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            PlacePrefab();
        }
    }
    private void RotatePrefab()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            //TODO: smooth rotation
            transform.Rotate(0, 90, 0);
        }
    }

    private void CarryPrefab()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //Cast a ray, when it hits a collider -> return hitinfo
        if (Physics.Raycast(ray, out hit))
        {
            transform.position = hit.point;

            //Adjust building's rotation based on terrain
            //transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
        }
    }

    private void PlacePrefab()
    {
        GameObject building = Instantiate(prefab, transform.position, transform.rotation);
        //storages.Add(storageHouse);
        Destroy(gameObject);
    }

    private void CarryingPrefabChecks()
    {
        if (EventSystem.current.IsPointerOverGameObject() && Input.GetMouseButtonDown(0))
        {
            Destroy(gameObject);
        }
        if (Input.GetMouseButtonDown(1))
        {
            Destroy(gameObject);
        }
    }
}
