using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeSpeedUp : MonoBehaviour
{
    //Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.K))
        {
            Time.timeScale = 6 - Time.timeScale;
            Debug.Log(Time.timeScale);
        }

    }
}
