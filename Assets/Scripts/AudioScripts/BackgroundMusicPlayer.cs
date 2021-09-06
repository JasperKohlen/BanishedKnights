using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusicPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource musicPlayer;
    [SerializeField] private AudioClip[] themes;
    private int selectTheme;


    // Start is called before the first frame update
    void Start()
    {
        selectTheme = Random.Range(0, themes.Length - 1);
        musicPlayer.clip = themes[selectTheme];
        musicPlayer.Play();

        Invoke("PlayNextTheme", themes[selectTheme].length);
    }

    void PlayNextTheme()
    {
        //Make sure music doesn't play twice
        musicPlayer.Stop();

        selectTheme = Random.Range(0, themes.Length - 1);
        musicPlayer.clip = themes[selectTheme];
        musicPlayer.Play();

        Invoke("PlayNextTheme", themes[selectTheme].length);
    }
}
