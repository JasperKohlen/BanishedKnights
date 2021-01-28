using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour
{
    RemovableSelection removableSelection;
    SelectedDictionary selectedTable;

    public GameObject removeDestroyPanel;
    private void Start()
    {
        removableSelection = EventSystem.current.gameObject.GetComponent<RemovableSelection>();
        selectedTable = EventSystem.current.gameObject.GetComponent<SelectedDictionary>();

    }
    //Open the menu with all remove and destroy buttons
    public void HandleRemoveDestroyPanel()
    {
        if (removeDestroyPanel.activeSelf)
        {
            Debug.Log("Turning off menu");
            removeDestroyPanel.SetActive(false);
        }
        else if (!removeDestroyPanel.activeSelf)
        {
            Debug.Log("Turning on menu");
            removeDestroyPanel.SetActive(true);
        }
    }

    //Enable/Disable resource removal selection
    public void HandleRemovableSelection()
    {
        if (removableSelection.enabled == true)
        {
            removableSelection.enabled = false;
            selectedTable.DeselectAll();
        }
        else 
        {
            removableSelection.enabled = true;
        }
    }
}
