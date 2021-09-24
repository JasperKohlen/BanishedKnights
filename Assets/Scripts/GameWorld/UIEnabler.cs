using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIEnabler : MonoBehaviour
{
    [SerializeField] private GameObject canvas;
    // Start is called before the first frame update
    void Start()
    {
        canvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (SettleChecker.settled == true)
        {
            canvas.SetActive(true);
            Debug.Log("Settled is " + SettleChecker.settled);

            Destroy(this);
        }
    }
}
