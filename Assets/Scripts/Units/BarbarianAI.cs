using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class BarbarianAI : UnitHealth
{
    private SoldierDictionary enemies;
    private GameObject currentTarget;
    private NavMeshAgent agent;
    private Animator anim;
    private bool aggro;
    private float radius = 100f;
    private float attackTimer;

    [SerializeField] private Transform camp;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        enemies = EventSystem.current.GetComponent<SoldierDictionary>();
        agent = GetComponent<NavMeshAgent>();
        attackTimer = statsSO.attackDelay;
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<NavMeshAgent>().isStopped = false;
        Collider[] hits = Physics.OverlapSphere(camp.position, 20f, LayerMask.GetMask("Units"));
        if (hits.Any(s => s.gameObject.layer == 6))
        {
            FindEnemies();
        }
        else
        {
            aggro = false;
            BackToCamp();
        }
    }
    private void FindEnemies()
    {
        //TODO: Doesnt target workers yet
        if (enemies.GetTable().Count <= 0)
        {
            aggro = false;
            BackToCamp();
        }
        else
        {
            foreach (var enemy in enemies.GetTable().ToList())
            {
                var enemyDistanceToCamp = (camp.position - enemy.Value.transform.position).magnitude;
                var distanceToEnemy = (transform.position - enemy.Value.transform.position).magnitude;

                if (enemyDistanceToCamp <= statsSO.aggroRange)
                {
                    SetAggro(enemy.Value);
                    if (distanceToEnemy <= statsSO.atkRange)
                    {
                        GetComponent<NavMeshAgent>().isStopped = true;
                        agent.SetDestination(transform.position);
                        Attack();
                    }
                }
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
        attackTimer += Time.deltaTime;
        if (attackTimer >= statsSO.attackDelay)
        {
            anim.Play("BarbarianSwordSlash");
            Debug.Log("Barbarian: Attacking!");
            currentTarget.GetComponent<Soldier>().TakeDamage(statsSO.attackDmg);
            attackTimer = 0;
        }
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

    public override void Die()
    {
        Destroy(gameObject);
    }
}
