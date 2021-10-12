using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIStructureCostTxtHandler : MonoBehaviour
{
    [SerializeField] private BlueprintSO blueprintRecipe;
    [SerializeField] private TMP_Text logsCost;
    [SerializeField] private TMP_Text cobblesCost;
    // Start is called before the first frame update
    void Start()
    {
        logsCost.text = "Logs: " + blueprintRecipe.logsNeeded;
        cobblesCost.text = "Cobbles: " + blueprintRecipe.cobblesNeeded;
    }
}
