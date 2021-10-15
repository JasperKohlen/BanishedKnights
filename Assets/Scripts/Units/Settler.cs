using CodeMonkey.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settler : MonoBehaviour
{
    [SerializeField] private Transform housePrefab;
    [SerializeField] private Transform storagePrefab;
    [SerializeField] private BlueprintSO houseSO;
    [SerializeField] private BlueprintSO storageSO;

    private GridSystem gridSystem;
    private void Start()
    {
        gridSystem = GameObject.Find("WorldManager").GetComponent<GridSystem>();
    }
    public void ConsumeSettler()
    {
        PlacePrefab(housePrefab, houseSO, 5, 0);
        PlacePrefab(housePrefab, houseSO, -3, 0);
        PlacePrefab(storagePrefab, storageSO, 0, 3);
        SettleChecker.settled = true;

        Destroy(gameObject);
    }

    //Instantiates building around settler position upon settling
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
