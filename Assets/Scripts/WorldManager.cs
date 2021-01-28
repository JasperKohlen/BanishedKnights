using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager : MonoBehaviour
{
    //300 -300 range
    public GameObject prefab;
    private Vector3 randomPosition;
    private Quaternion randomRotation;
    private float timer;
    private float time;
    public float minTime;
    public float maxTime;
    // Start is called before the first frame update
    void Start()
    {
        time = Random.Range(minTime, maxTime);
    }

    // Update is called once per frame
    void Update()
    {
        //Counts up every second
        timer += Time.deltaTime;
        //If timer counts higher than random time
        if (timer > time)
        {
            SpawnCloud();
            time = Random.Range(minTime, maxTime);
            timer = 0;
        }
    }

    void SpawnCloud()
    {
        randomPosition = new Vector3(-300, 90, Random.Range(300, -300));
        //randomRotation = Quaternion.Euler(0, Random.Range(0, 360), 0);

        //Instantiate with randomPosition on Z and random rotation
        Instantiate(prefab, randomPosition, randomRotation);
    }
}
