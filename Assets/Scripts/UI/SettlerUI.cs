using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettlerUI : MonoBehaviour
{
    private UIAudio uiAudio;
    [SerializeField] private GameObject confirmationBox;
    [SerializeField] private Settler settler;
    [SerializeField] private GameObject settleBtn;

    private void Start()
    {
        uiAudio = FindObjectOfType<UIAudio>();
    }

    public void SettleButtonClicked()
    {
        uiAudio.PlayBtnClick();
        confirmationBox.SetActive(true);
    }

    public void ConfirmSettle()
    {
        uiAudio.PlaySettleSound();
        confirmationBox.SetActive(false);
        settler.ConsumeSettler();
    }

    public void RefuseSettle()
    {
        uiAudio.PlayBtnClick();
        confirmationBox.SetActive(false);
    }
}
