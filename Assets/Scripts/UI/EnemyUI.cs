using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUI : MonoBehaviour
{
    private Camera camToLookAt;

    private void Start()
    {
        camToLookAt = Camera.main;
    }

    private void Update()
    {
        transform.LookAt(camToLookAt.transform);
        transform.rotation = Quaternion.LookRotation(camToLookAt.transform.forward);
    }
}
