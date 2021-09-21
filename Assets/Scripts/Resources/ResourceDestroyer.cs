using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceDestroyer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Removable"))
        {
            Debug.Log(other.name);
            Destroy(other.gameObject);
        }
    }
}
