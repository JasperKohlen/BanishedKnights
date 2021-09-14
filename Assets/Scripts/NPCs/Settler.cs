using CodeMonkey.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settler : MonoBehaviour
{
    [SerializeField] private Transform housePrefab;
    [SerializeField] private Transform storagePrefab;
    [SerializeField] private Transform workerPrefab;
    [SerializeField] private BlueprintSO houseSO;
    [SerializeField] private BlueprintSO storageSO;

    Vector3 worker1Pos;
    Vector3 worker2Pos;
    Vector3 worker3Pos;
    Vector3 worker4Pos;

    private GridSystem gridSystem;
    private void Start()
    {
        gridSystem = GameObject.Find("WorldManager").GetComponent<GridSystem>();
    }
    public void ConsumeSettler()
    {
        DeterminePrefabPositions();

        PlacePrefab(housePrefab, houseSO, 5, 0);
        PlacePrefab(housePrefab, houseSO, -3, 0);
        PlacePrefab(storagePrefab, storageSO, 0, 3);

        Instantiate(workerPrefab, worker1Pos, Quaternion.identity);
        Instantiate(workerPrefab, worker2Pos, Quaternion.identity);
        Instantiate(workerPrefab, worker3Pos, Quaternion.identity);
        Instantiate(workerPrefab, worker4Pos, Quaternion.identity);

        Destroy(gameObject);
    }

    private void DeterminePrefabPositions()
    {
        worker1Pos = new Vector3(gameObject.transform.position.x + 1, gameObject.transform.position.y, gameObject.transform.position.z);
        worker2Pos = new Vector3(gameObject.transform.position.x - 1, gameObject.transform.position.y, gameObject.transform.position.z - 1);
        worker3Pos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z - 2);
        worker4Pos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
    }

    private void PlacePrefab(Transform structure, BlueprintSO sctructureSO, int xOffset, int zOffset)
    {
        gridSystem.grid.GetXZ(transform.position, out int x, out int z);
        Debug.Log("Settled on: " + x + ", " + z);
        Vector3 pos = gridSystem.grid.GetWorldPosition(x, z) +
                new Vector3(xOffset, 0, zOffset) * gridSystem.grid.GetCellSize();
        x += xOffset;
        z += zOffset;

        List<Vector2Int> gridPositions = sctructureSO.GetGridPositionsList(new Vector2Int(x, z), BlueprintSO.Dir.LEFT);
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
            //Place building and mark every gridtile as 'built'
            Transform builtTransform =
                Instantiate(
                    structure,
                    pos,
                    Quaternion.identity
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
        }
    }
}
