using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using System;

public class CustomGrid<TGridObject>
{
    public event EventHandler<OnGridObjectChangedEventArgs> OnGridObjectChanged;
    public class OnGridObjectChangedEventArgs : EventArgs
    {
        public int x;
        public int z;
    }

    private int width;
    private int height;
    private int length;
    private float cellSize;
    private Vector3 originPosition;
    private TGridObject[, ,] gridArray;
    private TextMesh[, ,] debugTextArray;

    public CustomGrid(int width, int length, float cellSize, Vector3 originPosition, Func<CustomGrid<TGridObject>, int, int, TGridObject> createGridObject)
    {
        this.width = width;
        this.height = 10;
        this.length = length;
        this.cellSize = cellSize;
        this.originPosition = originPosition;

        gridArray = new TGridObject[width, height, length];
        debugTextArray = new TextMesh[width, height, length];
         
        for (int x = 0; x < gridArray.GetLength(0); x++)
        {
            for (int z = 0; z < gridArray.GetLength(2); z++)
            {
                gridArray[x, 0, z] = createGridObject(this, x, z);
                debugTextArray[x, 0, z] = UtilsClass.CreateWorldText(gridArray[x, 0, z].ToString(), null, GetWorldPosition(x,z) + new Vector3(cellSize, 0, cellSize) * .5f, 20, Color.white, TextAnchor.MiddleCenter);
            }
        }
        DrawLines();

        OnGridObjectChanged += (object sender, OnGridObjectChangedEventArgs eventArgs) =>
        {
            debugTextArray[eventArgs.x, 0, eventArgs.z].text = gridArray[eventArgs.x, 0, eventArgs.z]?.ToString();
        };

    }
    public CustomGrid(int width, int length)
    {
        this.width = width;
        this.height = 1;
        this.length = length;
        this.cellSize = 3f;
    }

    public void SetPosition(Vector3 originPosition)
    {
        this.originPosition = originPosition;
    }

    private void DrawLines()
    {
        for (int x = 0; x <= gridArray.GetLength(0); x++)
            Debug.DrawLine(GetWorldPosition(x, 0), GetWorldPosition(x, length), Color.white, 100f);
        for (int z = 0; z <= gridArray.GetLength(2); z++)
            Debug.DrawLine(GetWorldPosition(0, z), GetWorldPosition(width, z), Color.white, 100f);
    }

    public Vector3 GetWorldPosition(int x, int z)
    {
        return new Vector3(x, 0, z) * cellSize + originPosition;
    }

    public void GetXZ(Vector3 worldPosition, out int x, out int z)
    {
        x = Mathf.FloorToInt((worldPosition - originPosition).x / cellSize);
        z = Mathf.FloorToInt((worldPosition - originPosition).z / cellSize);
    }
    public void TriggerGridObjectChanged(int x, int z)
    {
        OnGridObjectChanged.Invoke(this, new OnGridObjectChangedEventArgs { x = x, z = z });
    }

    public void SetGridObject(int x, int z, TGridObject value)
    {
        if (x >= 0 && z >= 0 && x < width && z < length)
        {
            gridArray[x, 0, z] = value;
            debugTextArray[x, 0, z].text = gridArray[x, 0, z].ToString();
        }
    }

    public void SetGridObject(Vector3 worldPosition, TGridObject value)
    {
        int x, z;
        GetXZ(worldPosition, out x, out z);
        SetGridObject(x, z, value);
    }

    public TGridObject GetGridObject(int x, int z)
    {
        if (x >= 0 && z >= 0 && x < width && z < length)
        {
            return gridArray[x, 0, z];
        }
        else
        {
            return default;
        }
    }

    public TGridObject GetGridObject(Vector3 worldPosition)
    {
        int x, z;
        GetXZ(worldPosition, out x, out z);
        return GetGridObject(x, z);
    }
}
