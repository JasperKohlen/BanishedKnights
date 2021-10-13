using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    HarvestSelection removableSelection;

    [SerializeField] private GameObject removeBtn;
    [SerializeField] private GameObject removeDestroyPanel;
       
    [SerializeField] private GameObject storageMenu;
    [SerializeField] private GameObject globalStorageMenu;
    [SerializeField] private GameObject instructionsMenu;
    [SerializeField] private TMP_Text logsTxt;
    [SerializeField] private TMP_Text cobblesTxt;

    private BarracksController selectedBarracks;

    private void Start()
    {
        removableSelection = EventSystem.current.gameObject.GetComponent<HarvestSelection>();
    }

    //Open the menu with all remove and destroy buttons
    public void HandleHarvestPanel()
    {
        //Disable remove-panel
        if (removeDestroyPanel.activeSelf)
        {
            removableSelection.enabled = false;
            removeDestroyPanel.SetActive(false);
        }
        //Enable remove-panel
        else if (!removeDestroyPanel.activeSelf)
        {
            removeDestroyPanel.SetActive(true);
            removeBtn.GetComponent<Image>().color = Color.grey;
            removableSelection.enabled = true;
        }
    }

    public void HandleInstructionsMenu()
    {
        if (!instructionsMenu.activeSelf)
        {
            instructionsMenu.SetActive(true);
        }
        else
        {
            instructionsMenu.SetActive(false);
        }
    }
    public void HandleGlobalStorageMenu()
    {
        if (!globalStorageMenu.activeSelf)
        {
            globalStorageMenu.SetActive(true);
        }
        else
        {
            globalStorageMenu.SetActive(false);
        }
    }

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

    #endregion
}
