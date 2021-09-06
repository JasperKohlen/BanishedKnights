using System.Collections;
using System.Collections.Generic;
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

    #endregion
}
