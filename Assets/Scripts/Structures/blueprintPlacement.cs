using CodeMonkey.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//Starts dragging blueprint once it's spawned in
public class blueprintPlacement : MonoBehaviour
{
    [Header("Blueprint")]
    [SerializeField] private static blueprintPlacement instance;
    [SerializeField] private Transform prefab;

    [Header("Scriptable Object")]
    [SerializeField] private BlueprintSO blueprintSO;

    [Header("Child")]
    [SerializeField] private GameObject childObject;

    private RaycastHit hit;

    private GridSystem gridSystem;
    private BlueprintSO.Dir dir = BlueprintSO.Dir.LEFT;

    private void Start()
    {
        gridSystem = GameObject.Find("WorldManager").GetComponent<GridSystem>();
    }
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
            Vector2Int rotationOffset = blueprintSO.GetRotationOffset(dir);
            Vector3 placedObjectWorldPosition = transform.position +
                new Vector3(rotationOffset.x, 0, rotationOffset.y) * gridSystem.grid.GetCellSize();

            childObject.transform.Rotate(0, 90, 0);

            dir = BlueprintSO.GetNextDir(dir);
            Debug.Log(dir);
        }
    }

    private void CarryPrefab()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //Cast a ray, when it hits a collider -> return hitinfo
        if (Physics.Raycast(ray, out hit))
        {
            transform.position = hit.point;
        }
    }

    private void PlacePrefab()
    {
        gridSystem.grid.GetXZ(Mouse3D.GetMouseWorldPosition3D(), out int x, out int z);

        List<Vector2Int> gridPositions = blueprintSO.GetGridPositionsList(new Vector2Int(x, z), dir);
        GridObject gridObject = gridSystem.grid.GetGridObject(x, z);

        bool canBuild = true;
        foreach (var gridPos in gridPositions)
        {
            if (!gridSystem.grid.GetGridObject(gridPos.x, gridPos.y).CanBuild())
            {
                canBuild = false;
                break;
            }
        }

        if (canBuild)
        {
            //Get building rotation and offset as pivot changes during rotation
            Vector2Int rotationOffset = blueprintSO.GetRotationOffset(dir);
            Vector3 placedObjectWorldPosition = gridSystem.grid.GetWorldPosition(x, z) + 
                new Vector3(rotationOffset.x, 0, rotationOffset.y) * gridSystem.grid.GetCellSize();

            //Place building and mark every gridtile as 'built'
            blueprintSO.position = placedObjectWorldPosition;
            blueprintSO.rotation = Quaternion.Euler(0, blueprintSO.GetRotationAngle(dir), 0);
            Transform builtTransform = 
                Instantiate(
                    prefab, 
                    placedObjectWorldPosition, 
                    Quaternion.Euler(0, blueprintSO.GetRotationAngle(dir), 0)
                );

            foreach (var gridPos in gridPositions)
            {
                gridSystem.grid.GetGridObject(gridPos.x, gridPos.y).SetTransform(builtTransform);
            }
            gridObject.SetTransform(builtTransform);
        }
        else
        {
            UtilsClass.CreateWorldTextPopup("Cannot build in this location!", Mouse3D.GetMouseWorldPosition3D());
            CarryPrefab();
        }

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
