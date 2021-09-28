using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class GridSystem : MonoBehaviour
{
    public CustomGrid<GridObject> grid;
    // Start is called before the first frame update
    void Start()
    {
        grid = new CustomGrid<GridObject>(100, 100, 5f, new Vector3(-250,0,-250), (CustomGrid<GridObject> o, int x, int z) => new GridObject(o, x, z));
    }
}

public class GridObject
{
    private CustomGrid<GridObject> grid;
    private int x;
    private int z;
    private string coord;
    private Transform transform;

    public GridObject(CustomGrid<GridObject> grid, int x, int z)
    {
        this.grid = grid;
        this.x = x;
        this.z = z;
        coord = x + "," + z;
    }

    public void SetTransform(Transform transform)
    {
        this.transform = transform;
        if (grid.showDebug)
        {
            grid.TriggerGridObjectChanged(x, z);
        }
    }
    public void ClearTransform()
    {
        transform = null;
        if (grid.showDebug)
        {
            grid.TriggerGridObjectChanged(x, z);
        }
    }

    public bool CanBuild()
    {
        return transform == null;
    }

    public override string ToString()
    {
        return coord + "\n" + transform;
    }
}
