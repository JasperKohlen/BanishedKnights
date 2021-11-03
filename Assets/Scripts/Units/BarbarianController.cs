using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarbarianController : MonoBehaviour
{
    private WinGameCondition endCondition;
    private UIAudio uiAudio;
    private GameObject bgMusic;
    List<BarbarianAI> barbarians = new List<BarbarianAI>();
    // Start is called before the first frame update
    void Start()
    {
        uiAudio = FindObjectOfType<UIAudio>();
        endCondition = gameObject.GetComponent<WinGameCondition>();
        bgMusic = FindObjectOfType<BackgroundMusicPlayer>().gameObject;
    }

    public void AddBarbarian(BarbarianAI b)
    {
        barbarians.Add(b);
    }
    public void RemoveBarbarian(BarbarianAI b)
    {
        barbarians.Remove(b);
        if (barbarians.Count <= 0)
        {
            bgMusic.SetActive(false);
            uiAudio.PlayVictorySound();
            endCondition.WinGame();
        }
    }
}
