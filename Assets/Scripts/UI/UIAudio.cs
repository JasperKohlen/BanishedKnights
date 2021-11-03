using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAudio : MonoBehaviour
{
    [SerializeField] private AudioSource musicPlayer;
    [SerializeField] private AudioClip btnClickAudio;
    [SerializeField] private AudioClip buildingClickAudio;
    [SerializeField] private AudioClip settleAudio;
    [SerializeField] private AudioClip victorySfx;
    [SerializeField] private AudioClip loseSfx;
    [SerializeField] private AudioClip blueprintPlacementAudio;
    private float startVolume;

    private void Start()
    {
        startVolume = musicPlayer.volume;
    }

    public void PlayBtnClick()
    {
        musicPlayer.volume = startVolume;

        float randomPitch = Random.Range(0.8f, 1.5f);
        musicPlayer.clip = btnClickAudio;
        musicPlayer.pitch = randomPitch;
        musicPlayer.Play();
    }

    public void PlayBuildingClick()
    {
        musicPlayer.volume = startVolume;

        float randomPitch = Random.Range(0.8f, 1.5f);
        musicPlayer.clip = buildingClickAudio;
        musicPlayer.pitch = randomPitch;
        musicPlayer.Play();
    }

    public void PlayBlueprintPlacement()
    {
        musicPlayer.volume = startVolume;

        float randomPitch = Random.Range(0.7f, 1.2f);
        musicPlayer.clip = blueprintPlacementAudio;
        musicPlayer.pitch = randomPitch;
        musicPlayer.Play();
    }

    public void PlaySettleSound()
    {
        musicPlayer.clip = settleAudio;
        musicPlayer.Play();
    }

    public void PlayVictorySound()
    {
        musicPlayer.volume = 0.1f;
        musicPlayer.clip = victorySfx;
        musicPlayer.Play();
    }

    public void PlayLoseSound()
    {
        musicPlayer.volume = 0.4f;
        musicPlayer.clip = loseSfx;
        musicPlayer.Play();
    }
}
