using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllableUI : MonoBehaviour
{
    [SerializeField] private GameObject selectedImage;
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
    public void ShowSelectedUI()
    {
        selectedImage.SetActive(true);
    }
    public void HideSelectedUI()
    {
        selectedImage.SetActive(false);
    }
}
