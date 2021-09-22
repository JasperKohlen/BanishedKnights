using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ResourceDestroyer : MonoBehaviour
{
    SelectedDictionary selected;
    private void Start()
    {
        selected = EventSystem.current.GetComponent<SelectedDictionary>();    
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Removable"))
        {
            Debug.Log(other.name + " removed");
            if (selected.Contains(other.gameObject))
            {
                selected.Remove(other.gameObject);
            }
            Destroy(other.gameObject);
        }
    }
}
