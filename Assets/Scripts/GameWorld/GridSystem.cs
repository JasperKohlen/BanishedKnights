using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class GridSystem : MonoBehaviour
{
    [SerializeField] private Transform testPrefab;
    CustomGrid<GridObject> grid;
    //CustomGrid<StringGridObject> gridString;
    // Start is called before the first frame update
    void Start()
    {
        //gridString = new CustomGrid<StringGridObject>(10, 10, 5f, Vector3.zero, (CustomGrid<StringGridObject> o, int x, int z) => new StringGridObject(o, x, z));
        grid = new CustomGrid<GridObject>(10, 10, 5f, new Vector3(0,0,-20), (CustomGrid<GridObject> o, int x, int z) => new GridObject(o, x, z));
    }

    private void Update()
    {
        Vector3 position = Mouse3D.GetMouseWorldPosition3D();

        if (Input.GetMouseButtonDown(0))
        {
            grid.GetXZ(Mouse3D.GetMouseWorldPosition3D(), out int x, out int z);

            GridObject gridObject = grid.GetGridObject(x, z);
            if (gridObject.CanBuild())
            {
                Transform builtTransform = Instantiate(testPrefab, grid.GetWorldPosition(x, z), Quaternion.identity);
                gridObject.SetTransform(builtTransform);
            }
            else
            {
                UtilsClass.CreateWorldTextPopup("Cannot build in this location!", Mouse3D.GetMouseWorldPosition3D());
            }

        }
        //if (Input.GetMouseButtonDown(0)) grid.GetGridObject(position).ChangeValue();

        //if (Input.GetKeyDown(KeyCode.A)) gridString.GetGridObject(position).AddLetter("A");
        //if (Input.GetKeyDown(KeyCode.B)) gridString.GetGridObject(position).AddLetter("B");
        //if (Input.GetKeyDown(KeyCode.C)) gridString.GetGridObject(position).AddLetter("C");

        //if (Input.GetKeyDown(KeyCode.Alpha1)) gridString.GetGridObject(position).AddLetter("1");
        //if (Input.GetKeyDown(KeyCode.Alpha2)) gridString.GetGridObject(position).AddLetter("2");
        //if (Input.GetKeyDown(KeyCode.Alpha3)) gridString.GetGridObject(position).AddLetter("3");
    }
}

public class StringGridObject
{
    private CustomGrid<StringGridObject> grid;
    private int x;
    private int z;

    private string letters;
    private string numbers;
    public StringGridObject(CustomGrid<StringGridObject> grid, int x, int z)
    {
        this.grid = grid;
        this.x = x;
        this.z = z;
        letters = "";
        numbers = "";
    }

    public void AddLetter(string letter)
    {
        letters += letter;
        grid.TriggerGridObjectChanged(x, z);
    }

    public void AddNumber(string number)
    {
        numbers += number;
        grid.TriggerGridObjectChanged(x, z);
    }

    public override string ToString()
    {
        return letters + "\n" + numbers;
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
        coord = x + ", " + z;
    }

    public void SetTransform(Transform transform)
    {
        this.transform = transform;
            grid.TriggerGridObjectChanged(x,z);
    }
    public void ClearTransform()
    {
        transform = null;
            grid.TriggerGridObjectChanged(x,z);
    }

    public bool CanBuild()
    {
        return transform == null;
    }

    //public void ChangeValue()
    //{
    //    Debug.Log("Hoi");
    //    coord = "hoi";
    //    grid.TriggerGridObjectChanged(x,z);
    //}
    public override string ToString()
    {
        return coord + transform;
    }
}
