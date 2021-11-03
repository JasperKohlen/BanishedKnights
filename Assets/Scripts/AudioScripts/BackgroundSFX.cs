using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundSFX : MonoBehaviour
{
    [SerializeField] private AudioSource musicPlayer;
    [SerializeField] private AudioClip loopableForestSFX;
    // Start is called before the first frame update
    void Start()
    {
        musicPlayer.clip = loopableForestSFX;
        musicPlayer.loop = true;
        musicPlayer.Play();
    }
}
