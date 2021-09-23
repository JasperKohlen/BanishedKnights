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
    [SerializeField] private TMP_Text barracksOrderedSoldiersTxt;
    [SerializeField] private TMP_Text barracksLogsNeededTxt;
    [SerializeField] private TMP_Text barracksSoldierRecipeTxt;
    [SerializeField] private Button barracksOrderBtn;

    private BarracksController selectedBarracks;

    private void Start()
    {
        removableSelection = EventSystem.current.gameObject.GetComponent<RemovableSelection>();
        UnitTrainingCost();
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
        barracksLogsTxt.text = "Logs : " + localStorage.GetLogsCount();
    }

    public void OpenBarracksMenu(LocalStorageDictionary localStorage)
    {
        selectedBarracks = localStorage.gameObject.GetComponent<BarracksController>();
        barracksMenu.SetActive(true);
        barracksLogsTxt.text = "Logs : " + localStorage.GetLogsCount();
    }

    public void CloseBarracksMenu()
    {
        barracksMenu.SetActive(false);
    }

    public void UpdateBarracksMenu(OrderDictionary order)
    {
        barracksLogsNeededTxt.text = "of " + order.LogsNeeded();
        barracksOrderedSoldiersTxt.text = "Soldiers: " + order.GetTable().Count;
    }

    public void OrderSoldier()
    {
        int logsNeeded = barracksOrderBtn.GetComponent<OrderBtnComponent>().unit.logsRequired;
        selectedBarracks.MakeOrder(logsNeeded, barracksOrderBtn.GetComponent<OrderBtnComponent>().unit.unitType);
    }

    public void CancelSoldierOrder()
    {
        selectedBarracks.gameObject.GetComponent<OrderDictionary>().RemoveFirstOrder();
    }

    public void UnitTrainingCost()
    {
        barracksSoldierRecipeTxt.text = "Cost per unit: " + barracksOrderBtn.GetComponent<OrderBtnComponent>().unit.logsRequired + " logs and 1 worker";
    }

    #endregion
}
