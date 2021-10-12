using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeSpeedUp : MonoBehaviour
{
    //Update is called once per frame
    void Update()
    {
        //if (Input.GetKey(KeyCode.Alpha1))
        //{
        //    //Time.timeScale = 1 - Time.timeScale;
        //    Time.fixedDeltaTime = 1 - Time.fixedDeltaTime;
        //    Debug.Log(Time.timeScale);
        //}
        if (Input.GetKey(KeyCode.Alpha2))
        {
            //Time.timeScale = 3 - Time.timeScale;
            Time.fixedDeltaTime = 3 - Time.fixedDeltaTime;
            Debug.Log(Time.timeScale);
        }
        //if (Input.GetKey(KeyCode.Alpha3))
        //{
        //    Time.timeScale = 6 - Time.timeScale;
        //    //Time.fixedDeltaTime = 6 - Time.fixedDeltaTime;
        //    Debug.Log(Time.timeScale);
        //}
    }
}
