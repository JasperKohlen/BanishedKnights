using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameBtn : MonoBehaviour
{
    [SerializeField] private GameObject endScrn;
    private UIAudio uAudio;
    private void Start()
    {
        uAudio = FindObjectOfType<UIAudio>();
    }
    public void EndGame()
    {
        uAudio.PlayBtnClick();
        SceneManager.LoadSceneAsync("MainMenu");
    }

    public void ContinuePlaying()
    {
        uAudio.PlayBtnClick();
        endScrn.SetActive(false);
    }
}
