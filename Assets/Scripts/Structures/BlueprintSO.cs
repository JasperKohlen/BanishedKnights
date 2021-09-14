using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/BlueprintSO")]
public class BlueprintSO : ScriptableObject
{
    [Header("Recipe")]
    public int logsNeeded;
    public int cobblesNeeded;

    [Header("Size")]
    public int Height;
    public int Width;

    [Header("Resulting building")]
    public Transform Structure_to_build;
    [HideInInspector] public Vector3 position;

    public List<Vector2Int> GetGridPositionsList(Vector2Int offset, Dir dir)
    {
        List<Vector2Int> gridPositionsList = new List<Vector2Int>();
        switch (dir)
        {
            default:
            case Dir.LEFT:
            case Dir.RIGHT:
                for (int x = 0; x < Width; x++)
                {
                    for (int z = 0; z < Height; z++)
                    {
                        gridPositionsList.Add(offset + new Vector2Int(x, z));
                    }
                }
                break;
            case Dir.DOWN:
            case Dir.UP:
                for (int x = 0; x < Height; x++)
                {
                    for (int z = 0; z < Width; z++)
                    {
                        gridPositionsList.Add(offset + new Vector2Int(x, z));
                    }
                }
                break;
        }
        return gridPositionsList;
    }

    public int GetRotationAngle(Dir dir)
    {
        switch (dir)
        {
            default:
            case Dir.LEFT:  return 0;
            case Dir.DOWN:  return 90;
            case Dir.RIGHT: return 180;
            case Dir.UP:    return 270;
        }
    }

    //Object rotates around pivot, therefore object needs to be shifted to account for the pivot
    public Vector2Int GetRotationOffset(Dir dir)
    {
        switch (dir)
        {
            default:
            case Dir.LEFT:  return new Vector2Int(0, 0);
            case Dir.DOWN:  return new Vector2Int(0, Width);
            case Dir.RIGHT: return new Vector2Int(Width, Height);
            case Dir.UP:    return new Vector2Int(Height, 0);
        }
    }

    public static Dir GetNextDir(Dir dir)
    {
        switch (dir)
        {
            default:
            case Dir.LEFT:   return Dir.DOWN;
            case Dir.DOWN:   return Dir.RIGHT;
            case Dir.RIGHT:  return Dir.UP;
            case Dir.UP:     return Dir.LEFT;
        }
    }

    public enum Dir
    {
        DOWN,
        LEFT,
        RIGHT,
        UP
    }
}
