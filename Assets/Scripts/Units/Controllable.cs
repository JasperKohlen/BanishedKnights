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

        if (Physics.Raycast(ray, out hit) )
        {
            if (hit.collider.gameObject.GetComponent<BarbarianAI>() && gameObject.GetComponent<Soldier>())
            {
                gameObject.GetComponent<Soldier>().SetTarget(hit.collider.gameObject);
            }
            else
            {
                if (gameObject.GetComponent<Soldier>())
                {
                    gameObject.GetComponent<Soldier>().SetTarget(null);
                }
                agent.destination = hit.point;
            }
        }
    }
}
