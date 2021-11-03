using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeSpeedUp : MonoBehaviour
{
    [SerializeField] private GameObject pauseScrn;
    private float lastTimeScale;
    private float fixedDeltaTime;
    private bool paused = false;
    void Awake()
    {
        Time.timeScale = 1.0f;
        // Make a copy of the fixedDeltaTime, it defaults to 0.02f, but it can be changed in the editor
        this.fixedDeltaTime = Time.fixedDeltaTime;
    }

    //Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            if (Time.timeScale == 1.0f)
                Time.timeScale = 4.0f;
            else
                Time.timeScale = 1.0f;
            // Adjust fixed delta time according to timescale
            // The fixed delta time will now be 0.02 real-time seconds per frame
            Time.fixedDeltaTime = this.fixedDeltaTime * Time.timeScale;
        }
        if (Input.GetKeyDown(KeyCode.Escape) && paused == false)
        {
            pauseScrn.SetActive(true);
            paused = true;
            lastTimeScale = Time.timeScale;
            Time.timeScale = 0;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && paused == true)
        {
            pauseScrn.SetActive(false);
            paused = false;
            Time.timeScale = lastTimeScale;
        }
    }
}
