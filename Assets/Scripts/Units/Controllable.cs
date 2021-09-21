using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Controllable : MonoBehaviour
{
    private NavMeshAgent agent;

    private RaycastHit hit;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    public void MoveTowardsClick()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            Debug.Log("Moving...");
            agent.destination = hit.point;
        }
    }
}
