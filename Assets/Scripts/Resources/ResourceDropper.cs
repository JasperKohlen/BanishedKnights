using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class ResourceDropper : MonoBehaviour
{
    public AudioSource source;
    [SerializeField] private AudioClip[] breakSounds;
    private float randomPitch;
    private int selectSound;
    void Start()
    {
        randomPitch = Random.Range(0.25f, 2f);
        selectSound = Random.Range(0, breakSounds.Length - 1);
    }

    //Destroy resource
    public void Break(HarvestableComponent target)
    {
        target.gameObject.GetComponent<MeshRenderer>().enabled = false;
        target.gameObject.GetComponent<BoxCollider>().enabled = false;
        target.harvested = true;

        source.clip = breakSounds[selectSound];
        source.pitch = randomPitch;
        source.Play();

        Destroy(target.gameObject, source.clip.length);

    }
}
