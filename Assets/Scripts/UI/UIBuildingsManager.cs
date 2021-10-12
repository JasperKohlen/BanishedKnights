using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIBuildingsManager : MonoBehaviour
{
    HarvestSelection removableSelection;

    [SerializeField] private GameObject removeBtn;
    [SerializeField] private GameObject removeDestroyPanel;

    [SerializeField] private GameObject houseBlueprint;
    [SerializeField] private GameObject storageBlueprint;
    [SerializeField] private GameObject barracksBlueprint;
    private void Start()
    {
        removableSelection = EventSystem.current.gameObject.GetComponent<HarvestSelection>();
    }
    public void SpawnHouseBlueprint()
    {
        //Disable panels to prevent deselection
        removableSelection.enabled = false;
        removeDestroyPanel.SetActive(false);
        Instantiate(houseBlueprint);
    }

    public void SpawnStorageBlueprint()
    {
        //Disable panels to prevent deselection
        removableSelection.enabled = false;
        removeDestroyPanel.SetActive(false);
        Instantiate(storageBlueprint);
    }

    public void SpawnBarracksBlueprint()
    {
        //Disable panels to prevent deselection
        removableSelection.enabled = false;
        removeDestroyPanel.SetActive(false);
        Instantiate(barracksBlueprint);
    }
}
