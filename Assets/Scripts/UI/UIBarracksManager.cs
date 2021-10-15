using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIBarracksManager : MonoBehaviour
{
    [SerializeField] private GameObject barracksMenu;
    
    [SerializeField] private TMP_Text LogsTxt;
    [SerializeField] private TMP_Text LogsNeededTxt;
    [SerializeField] private TMP_Text SwordmanAmountTxt;
    [SerializeField] private TMP_Text SwordmanCostTxt;
    [SerializeField] private TMP_Text BowmanAmountTxt;
    [SerializeField] private TMP_Text BowmanCostTxt;
    [SerializeField] private Button OrderSwordmanBtn;
    [SerializeField] private Button OrderBowmanBtn;

    private BarracksController selectedBarracks;
    private LocalStorageDictionary thisStorage;
    // Start is called before the first frame update
    void Start()
    {
        UnitTrainingCost();
    }

    public void OpenBarracksMenu(LocalStorageDictionary localStorage)
    {
        thisStorage = localStorage;
        selectedBarracks = localStorage.gameObject.GetComponent<BarracksController>();
        barracksMenu.SetActive(true);
        LogsTxt.text = "Logs : " + localStorage.GetLogsCount();
    }

    public void CloseBarracksMenu()
    {
        barracksMenu.SetActive(false);
    }

    public void UpdateBarracksMenu(OrderDictionary order)
    {
        LogsTxt.text = "Logs : " + thisStorage.GetLogsCount();
        LogsNeededTxt.text = "of " + order.LogsNeeded();
        SwordmanAmountTxt.text = "Swordmen: " + order.GetSwordmanCount();
        BowmanAmountTxt.text = "Bowmen: " + order.GetBowmanCount();
    }

    public void OrderSwordman()
    {
        selectedBarracks.MakeOrder(OrderSwordmanBtn.GetComponent<OrderBtnComponent>().unit.unitType);
    }
    public void OrderBowman()
    {
        selectedBarracks.MakeOrder(OrderBowmanBtn.GetComponent<OrderBtnComponent>().unit.unitType);
    }
    public void CancelSwordmanOrder()
    {
        selectedBarracks.gameObject.GetComponent<OrderDictionary>().RemoveFirstOrder(OrderSwordmanBtn.GetComponent<OrderBtnComponent>().unit.unitType);
    }
    public void CancelBowmanOrder()
    {
        selectedBarracks.gameObject.GetComponent<OrderDictionary>().RemoveFirstOrder(OrderBowmanBtn.GetComponent<OrderBtnComponent>().unit.unitType);
    }
    public void UnitTrainingCost()
    {
        SwordmanCostTxt.text = "Cost per unit: " + OrderSwordmanBtn.GetComponent<OrderBtnComponent>().unit.logsRequired + " logs and 1 worker";
        BowmanCostTxt.text = "Cost per unit: " + OrderBowmanBtn.GetComponent<OrderBtnComponent>().unit.logsRequired + " logs and 1 worker";
    }

}