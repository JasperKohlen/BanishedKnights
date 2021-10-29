using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoveControllables : MonoBehaviour
{
    private SelectedUnitDictionary selectedTable;

    void Start()
    {
        selectedTable = GetComponent<SelectedUnitDictionary>();
    }
    private void Update()
    {
        //If not dragging
        if (Input.GetMouseButtonDown(1))
        {
            if (selectedTable.ContainsAnyControllables())
            {
                MoveUnits();
            }
        }
    }

    void MoveUnits()
    {
        foreach (var unit in selectedTable.GetTable())
        {
            unit.Value.GetComponent<Controllable>().MoveTowardsClick();
        }
    }
}
