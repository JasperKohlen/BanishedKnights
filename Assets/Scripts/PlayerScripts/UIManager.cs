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

    public GameObject removeBtn;
    public GameObject removeDestroyPanel;

    public GameObject houseBlueprint;
    public GameObject storageBlueprint;

    public GameObject storageMenu;
    public TMP_Text storageTxt;

    private void Start()
    {
        removableSelection = EventSystem.current.gameObject.GetComponent<RemovableSelection>();
    }

    private void Update()
    {
        if (storageMenu.activeSelf)
        {

        }
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

    #endregion

    #region stats

    public void OpenStorageMenu(LocalStorageDictionary localStorage)
    {
        storageMenu.SetActive(true);
        storageTxt.text = "Logs : " + localStorage.GetTable().Count.ToString();
    }
    public void CloseStorageMenu()
    {
        storageMenu.SetActive(false);
    }

    public void UpdateStorage(LocalStorageDictionary localStorage)
    {
        storageTxt.text = "Logs : " + localStorage.GetTable().Count.ToString();
    }
    #endregion
}
