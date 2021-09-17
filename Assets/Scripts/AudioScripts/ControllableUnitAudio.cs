using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllableUnitAudio : MonoBehaviour
{
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip[] selectionSounds;
    [SerializeField] private AudioClip[] moveSounds;
    private int selectedSound;

    public void PlaySelectionSound()
    {
        selectedSound = Random.Range(0, selectionSounds.Length - 1);

        source.clip = selectionSounds[selectedSound];
        source.Play();
    }

    public void PlayMoveSound()
    {
        selectedSound = Random.Range(0, moveSounds.Length - 1);

        source.clip = moveSounds[selectedSound];
        source.Play();
    }
}
