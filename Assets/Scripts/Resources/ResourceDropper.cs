using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class ResourceDropper : MonoBehaviour
{
    public GameObject prefab;
    private Vector3 objectPosition;
    private SelectedDictionary selectedTable;
    private Worker worker;

    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip[] breakSounds;
    private float randomPitch;
    private int selectSound;
    void Start()
    {
        selectedTable = EventSystem.current.GetComponent<SelectedDictionary>();
        objectPosition = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 2.0f, gameObject.transform.position.z);
        randomPitch = Random.Range(0.25f, 2f);
        selectSound = Random.Range(0, breakSounds.Length - 1);
    }

    private void OnTriggerEnter(Collider other)
    {
        //Only remove removable when selected and the collider is a worker who is in the removing state.
        if (other.gameObject.tag.Equals("Worker") && selectedTable.Contains(gameObject) && other.GetComponent<Worker>().state == State.REMOVING)
        {
            selectedTable.Remove(gameObject);

            worker = other.gameObject.GetComponent<Worker>();

            GameObject resource = Instantiate(prefab, objectPosition, Quaternion.identity);
            worker.resourceToDeliver.Add(resource);

            PlayBreakSound();
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            gameObject.GetComponent<BoxCollider>().enabled = false;
            Destroy(gameObject, source.clip.length);
        }
    }

    private void PlayBreakSound()
    {
        source.clip = breakSounds[selectSound];
        source.pitch = randomPitch;
        source.Play();
    }
}
