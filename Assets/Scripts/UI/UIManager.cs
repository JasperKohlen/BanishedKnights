using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    RemovableSelection removableSelection;

    [SerializeField] private GameObject removeBtn;
    [SerializeField] private GameObject removeDestroyPanel;
    
    [SerializeField] private GameObject houseBlueprint;
    [SerializeField] private GameObject storageBlueprint;
    [SerializeField] private GameObject barracksBlueprint;
    
    [SerializeField] private GameObject storageMenu;
    [SerializeField] private GameObject barracksMenu;
    [SerializeField] private TMP_Text logsTxt;
    [SerializeField] private TMP_Text cobblesTxt;
    [SerializeField] private TMP_Text barracksLogsTxt;
    [SerializeField] private TMP_Text barracksCobblesTxt;
    [SerializeField] private Button barracksOrderBtn;

    private BarracksController selectedBarracks;

    private void Start()
    {
        removableSelection = EventSystem.current.gameObject.GetComponent<RemovableSelection>();
    }

    //Open the menu with all remove and destroy buttons
    public void HandleRemoveDestroyPanel()
    {
        //Disable remove-panel
        if (removeDestroyPanel.activeSelf)
        {
            removeBtn.GetComponent<Image>().color = Color.white;
            removableSelection.enabled = false;
            removeDestroyPanel.SetActive(false);
        }
        //Enable remove-panel
        else if (!removeDestroyPanel.activeSelf)
        {
            removeDestroyPanel.SetActive(true);
        }
    }

    //Enable/Disable resource removal selection
    public void HandleRemovableSelection()
    {
        //Disable removable selection
        if (removableSelection.enabled == true)
        {
            removeBtn.GetComponent<Image>().color = Color.white;
            removableSelection.enabled = false;
        }
        //Enable removable selection
        else
        {
            removableSelection.enabled = true;
            removeBtn.GetComponent<Image>().color = Color.grey;
        }
    }

    #region Buildings
    public void SpawnHouseBlueprint()
    {
        removeBtn.GetComponent<Image>().color = Color.white;

        //Disable panels to prevent deselection
        removableSelection.enabled = false;
        removeDestroyPanel.SetActive(false);
        Instantiate(houseBlueprint);
    }

    public void SpawnStorageBlueprint()
    {
        removeBtn.GetComponent<Image>().color = Color.white;

        //Disable panels to prevent deselection
        removableSelection.enabled = false;
        removeDestroyPanel.SetActive(false);
        Instantiate(storageBlueprint);
    }

    public void SpawnBarracksBlueprint()
    {
        removeBtn.GetComponent<Image>().color = Color.white;

        //Disable panels to prevent deselection
        removableSelection.enabled = false;
        removeDestroyPanel.SetActive(false);
        Instantiate(barracksBlueprint);
    }

    #endregion

    #region structure stats

    public void OpenStorageMenu(LocalStorageDictionary localStorage)
    {
        storageMenu.SetActive(true);
        logsTxt.text = "Logs : " + localStorage.GetLogsCount();
        cobblesTxt.text = "Cobbles : " + localStorage.GetCobblesCount();
    }

    public void CloseStorageMenu()
    {
        storageMenu.SetActive(false);
    }

    public void UpdateLocalStorage(LocalStorageDictionary localStorage)
    {
        logsTxt.text = "Logs : " + localStorage.GetLogsCount();
        cobblesTxt.text = "Cobbles : " + localStorage.GetCobblesCount();
    }

    public void OpenBarracksMenu(LocalStorageDictionary localStorage)
    {
        selectedBarracks = localStorage.gameObject.GetComponent<BarracksController>();
        barracksMenu.SetActive(true);
        barracksLogsTxt.text = "Logs : " + localStorage.GetLogsCount();
        barracksCobblesTxt.text = "Cobbles : " + localStorage.GetCobblesCount();
    }

    public void CloseBarracksMenu()
    {
        barracksMenu.SetActive(false);
    }

    public void UpdateBarracksMenu(LocalStorageDictionary localStorage)
    {
        barracksLogsTxt.text = "Logs : " + localStorage.GetLogsCount();
        barracksCobblesTxt.text = "Cobbles : " + localStorage.GetCobblesCount();
    }

    public void OrderSoldier()
    {
        int logsNeeded = barracksOrderBtn.GetComponent<OrderBtnComponent>().resultingUnit.logsRequired;
        selectedBarracks.MakeOrder(logsNeeded, barracksOrderBtn.GetComponent<OrderBtnComponent>().resultingUnit.unitType);
    }

    #endregion
}
