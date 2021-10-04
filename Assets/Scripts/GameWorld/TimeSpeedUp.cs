using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeSpeedUp : MonoBehaviour
{
    //Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            Time.timeScale = 4.0f - Time.timeScale;
            Debug.Log(Time.timeScale);
        }
    }
}
