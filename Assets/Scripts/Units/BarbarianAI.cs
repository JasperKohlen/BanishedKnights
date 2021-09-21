using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class BarbarianAI : MonoBehaviour
{
    [SerializeField] private UnitStats stats;
    private SoldierDictionary enemies;
    private GameObject currentTarget;
    private NavMeshAgent agent;
    private bool aggro;
    private float radius = 100f;

    [SerializeField] private Transform camp;
    // Start is called before the first frame update
    void Start()
    {
        enemies = EventSystem.current.GetComponent<SoldierDictionary>();
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        FindEnemies();

    }
    private void FindEnemies()
    {
        //Doesnt target workers yet
        foreach (var enemy in enemies.GetTable())
        {
            var distanceToCamp = (camp.position - enemy.Value.transform.position).magnitude;
            var distanceToEnemy = (transform.position - enemy.Value.transform.position).magnitude;

            if (distanceToCamp <= stats.aggroRange)
            {
                SetAggro(enemy.Value);
                if (distanceToEnemy <= stats.atkRange)
                {
                    Debug.Log("Barbarian: Attacking!");
                    enemy.Value.GetComponent<Soldier>().TakeDamage(stats.attackDmg);
                }
            }
            else
            {
                BackToCamp();
                aggro = false;
            }
        }
    }
    private void SetAggro(GameObject target)
    {
        currentTarget = target;
        agent.SetDestination(currentTarget.transform.position);
        aggro = true;
    }

    private void Attack()
    {
        currentTarget.GetComponent<Soldier>().TakeDamage(stats.attackDmg);
    }

    private void BackToCamp()
    {
        agent.SetDestination(camp.position);
        if ((transform.position - camp.position).magnitude <= 20 && !aggro)
        {
            //Back to business as usual
            Vector3 destination;
            destination = Random.insideUnitSphere * radius + camp.position;
            agent.SetDestination(destination);
        }
    }
}
