using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class Soldier : UnitHealth
{
    private Animator anim;
    private SoldierDictionary soldiers;
    private SelectedUnitDictionary selectedUnits;
    private GameObject currentTarget;
    private float attackTimer;

    private NavMeshAgent agent;
    private Vector3 destination;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        soldiers = EventSystem.current.GetComponent<SoldierDictionary>();
        selectedUnits = FindObjectOfType<SelectedUnitDictionary>();
        soldiers.Add(gameObject);

        agent = GetComponent<NavMeshAgent>();
        attackTimer = statsSO.attackDelay;
    }

    private void Update()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, statsSO.aggroRange);
        float distanceToEnemy;
        destination = transform.position;
        GetComponent<NavMeshAgent>().isStopped = false;

        if (currentTarget != null)
        {
            destination = currentTarget.transform.position;
            agent.SetDestination(destination);

            distanceToEnemy = (transform.position - destination).magnitude;
            if (distanceToEnemy <= statsSO.atkRange)
            {
                GetComponent<NavMeshAgent>().isStopped = true;
                destination = transform.position;
                Attack();
            }
        }
        else if (hits.ToList().Any(s => s.gameObject.tag.Contains("Enemy")))
        {
            List<GameObject> enemies = new List<GameObject>();
            foreach (var item in hits.ToList())
            {
                if (item.gameObject.tag.Contains("Enemy"))
                {
                    Debug.Log("Found enemies! " + item.gameObject.name);
                    enemies.Add(item.gameObject);
                }
            }
            enemies = enemies.OrderBy(s => Vector3.Distance(gameObject.transform.position, s.transform.position)).ToList();

            currentTarget = enemies.First().gameObject;
        }
    }

    public override void Die()
    {
        soldiers.Remove(gameObject);
        selectedUnits.Remove(gameObject);
        Destroy(gameObject);
    }

    public void SetTarget(GameObject target)
    {
        //target.transform.Find("TargetedImage").gameObject.SetActive(true);
        currentTarget = target;
    }

    private void Attack()
    {
        attackTimer += Time.deltaTime;

        if (attackTimer >= statsSO.attackDelay)
        {
            if (statsSO.unitType.GetComponent<BowmanComponent>())
            {
                anim.Play("bowSlash");

            }
            if (statsSO.unitType.GetComponent<SwordmanComponent>())
            {
                anim.Play("SwordSlash");
            }
            currentTarget.GetComponent<BarbarianAI>().TakeDamage(statsSO.attackDmg);
            attackTimer = 0;
        }
    }
}
