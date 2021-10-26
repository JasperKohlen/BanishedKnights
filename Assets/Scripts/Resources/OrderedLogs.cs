using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderedLogs : MonoBehaviour
{
    private int pickupUpLogs;
    // Start is called before the first frame update
    void Start()
    {
        pickupUpLogs = 0;
    }

    public void IncreasePickedUpLogs()
    {
        pickupUpLogs++;
    }

    public int GetPickedUpLogs()
    {
        return pickupUpLogs;
    }
}
