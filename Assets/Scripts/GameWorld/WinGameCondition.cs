using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinGameCondition : MonoBehaviour
{
    [SerializeField] private GameObject endScreen;
    public void WinGame()
    {
        endScreen.SetActive(true);
    }
}
