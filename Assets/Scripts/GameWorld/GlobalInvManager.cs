using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class GlobalInvManager : MonoBehaviour
{
    GlobalInventoryDictionary global;
    StorageBuildingsDictionary storages;
    [SerializeField] private TMP_Text logsTxt;
    [SerializeField] private TMP_Text cobblesTxt;
    [SerializeField] private TMP_Text workersTxt;
    [SerializeField] private TMP_Text soldiersTxt;

    private int logsCount;
    private int cobblesCount;
    private int workersCount;
    private int soldiersCount;

    // Start is called before the first frame update
    void Start()
    {
        global = EventSystem.current.GetComponent<GlobalInventoryDictionary>();
        storages = EventSystem.current.GetComponent<StorageBuildingsDictionary>();
    }

    // Update is called once per frame
    void Update()
    {
        logsCount = 0;
        cobblesCount = 0;
        workersCount = 0;
        soldiersCount = 0;

        GetResources();
        GetWorkerCount();
        GetSoldiersCount();
        UpdateTexts();
    }

    private void UpdateTexts()
    {
        logsTxt.text = "Logs: " + logsCount;
        cobblesTxt.text = "Cobbles: " + cobblesCount;
        workersTxt.text = "Workers: " + workersCount;
        soldiersTxt.text = "Soldiers: " + soldiersCount;
    }

    private void GetSoldiersCount()
    {
        List<Soldier> soldiers = FindObjectsOfType<Soldier>().ToList();
        foreach (var item in soldiers)
        {
            soldiersCount++;
        }
    }

    private void GetWorkerCount()
    {
        List<WorkerScript> workers = FindObjectsOfType<WorkerScript>().ToList();
        foreach (var item in workers)
        {
            workersCount++;
        }
    }

    private void GetResources()
    {
        foreach (var storage in storages.GetTable())
        {
            foreach (var resource in storage.Value.GetComponent<LocalStorageDictionary>().GetTable())
            {
                if (resource.Value.GetComponent<LogComponent>())
                {
                    logsCount++;
                }
                if (resource.Value.GetComponent<CobbleComponent>())
                {
                    cobblesCount++;
                }
            }
        }
    }
}
