using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameBtn : MonoBehaviour
{
    [SerializeField] private GameObject endScrn;
    public void EndGame()
    {
        SceneManager.LoadSceneAsync("MainMenu");
    }

    public void ContinuePlaying()
    {
        endScrn.SetActive(false);
    }
}
