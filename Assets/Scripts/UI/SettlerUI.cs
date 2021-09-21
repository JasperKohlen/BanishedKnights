using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettlerUI : MonoBehaviour
{
    [SerializeField] private GameObject confirmationBox;
    [SerializeField] private Settler settler;
    [SerializeField] private GameObject settleBtn;

    public void SettleButtonClicked()
    {
        confirmationBox.SetActive(true);
    }

    public void ConfirmSettle()
    {
        confirmationBox.SetActive(false);
        settler.ConsumeSettler();
    }

    public void RefuseSettle()
    {
        confirmationBox.SetActive(false);
    }
}
