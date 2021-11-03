using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerAudio : MonoBehaviour
{
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip dropSound;

    public void PlayDropSound()
    {
        float randomPitch = Random.Range(0.8f, 1.3f);
        source.clip = dropSound;
        source.pitch = randomPitch;
        source.Play();
    }
}
