using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CivDropper : MonoBehaviour
{
    public GameObject civ;
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(civ, transform.position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
