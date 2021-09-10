using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettlerUI : MonoBehaviour
{
    [SerializeField] private GameObject confirmationBox;
    [SerializeField] private Settler settler;
    [SerializeField] private GameObject settleBtn;
    private Camera camToLookAt;

    private void Start()
    {
        camToLookAt = Camera.main;
    }

    private void Update()
    {
        transform.LookAt(camToLookAt.transform);
        transform.rotation = Quaternion.LookRotation(camToLookAt.transform.forward);
    }

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
