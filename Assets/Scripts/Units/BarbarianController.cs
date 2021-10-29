using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarbarianController : MonoBehaviour
{
    private WinGameCondition endCondition;
    List<BarbarianAI> barbarians = new List<BarbarianAI>();
    // Start is called before the first frame update
    void Start()
    {
        endCondition = gameObject.GetComponent<WinGameCondition>();
    }

    public void AddBarbarian(BarbarianAI b)
    {
        barbarians.Add(b);
    }
    public void RemoveBarbarian(BarbarianAI b)
    {
        barbarians.Remove(b);
        if (barbarians.Count <= 0)
        {
            endCondition.WinGame();
        }
    }
}
