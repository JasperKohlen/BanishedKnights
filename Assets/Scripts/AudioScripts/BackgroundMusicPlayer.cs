using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusicPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource musicPlayer;
    [SerializeField] private AudioClip theme;
    private int selectTheme;


    // Start is called before the first frame update
    void Start()
    {
        musicPlayer.clip = theme;
        musicPlayer.loop = true;
        musicPlayer.Play();
    }
}
