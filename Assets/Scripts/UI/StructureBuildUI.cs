using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StructureBuildUI : MonoBehaviour
{
    [SerializeField] private GameObject canvas;
    [SerializeField] private Image progressBar;
    private Camera camToLookAt;

    private bool quitting;

    private void Start()
    {
        camToLookAt = Camera.main;
        ShowProgressBar();
    }

    private void Update()
    {
        canvas.transform.LookAt(camToLookAt.transform);
        canvas.transform.rotation = Quaternion.LookRotation(camToLookAt.transform.forward);
            UpdateStructureProgress();
    }
    public void ShowProgressBar()
    {
        canvas.SetActive(true);
    }

    public void HideProgressBar()
    {
        canvas.SetActive(false);
    }

    public void UpdateStructureProgress()
    {
        float pct = GetComponent<StructureBuild>().GetPctComplete();
        progressBar.fillAmount = pct;
    }
    private void OnApplicationQuit()
    {
        quitting = true;
    }
    private void OnDestroy()
    {
        if (!quitting)
        {
            HideProgressBar();     
        }
    }
}
