using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoveControllables : MonoBehaviour
{
    private SelectedUnitDictionary selectedTable;
    // Start is called before the first frame update
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
        //REFACTOR
        GameObject.Find("Settler").GetComponent<ControllableUnitAudio>().PlayMoveSound();
        foreach (var unit in selectedTable.GetTable())
        {
            unit.Value.GetComponent<Controllable>().MoveTowardsClick();
        }
    }
}
