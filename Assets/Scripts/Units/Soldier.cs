using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Soldier : UnitHealth
{
    private SoldierDictionary soldiers;
    private SelectedUnitDictionary selectedUnits;

    // Start is called before the first frame update
    void Start()
    {
        soldiers = EventSystem.current.GetComponent<SoldierDictionary>();
        selectedUnits = EventSystem.current.GetComponent<SelectedUnitDictionary>();
        soldiers.Add(gameObject);
    }

    public override void Die()
    {
        soldiers.Remove(gameObject);
        selectedUnits.Remove(gameObject);
        Destroy(gameObject);
    }
}
