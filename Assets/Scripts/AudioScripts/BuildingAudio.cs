using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingAudio : MonoBehaviour
{
    [SerializeField] private AudioClip[] houseCompletionAudio;
    [SerializeField] private AudioSource source;

    public void Start()
    {
        int selected = Random.Range(0, 1);
        float randomPitch = Random.Range(0.8f, 1.5f);
        source.clip = houseCompletionAudio[selected];
        source.pitch = randomPitch;
        Debug.Log("Playing: " + source.clip + " | pitch: " + source.pitch);
        source.Play();
    }
}
