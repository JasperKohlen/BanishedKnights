using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public abstract class Labourer : MonoBehaviour, IGoap
{
    private SelectedDictionary selected;
    [HideInInspector] public ToBuildDictionary structsToBuild;
    [HideInInspector] public WorkerInventory inv;
    private NavMeshAgent agent;

    private void Start()
    {
        structsToBuild = EventSystem.current.GetComponent<ToBuildDictionary>();
        selected = EventSystem.current.GetComponent<SelectedDictionary>();
        agent = GetComponent<NavMeshAgent>();
        inv = GetComponent<WorkerInventory>();

        if (inv == null)
        {
            inv = gameObject.AddComponent<WorkerInventory>();
        }
    }
    public void actionsFinished()
    {
        Debug.Log("<color=blue>Actions completed</color>");
    }

    public abstract HashSet<KeyValuePair<string, object>> createGoalState();

    public HashSet<KeyValuePair<string, object>> getWorldState()
    {
        HashSet<KeyValuePair<string, object>> worldData = new HashSet<KeyValuePair<string, object>>();

        worldData.Add(new KeyValuePair<string, object>("resourcesSelected", selected.GetTable().Count > 0));
        worldData.Add(new KeyValuePair<string, object>("structuresToBuild", structsToBuild.GetTable().Count > 0));
        worldData.Add(new KeyValuePair<string, object>("holdingResource", inv.HoldingResource()));

        return worldData;
    }

    public bool moveAgent(GoapAction nextAction)
    {
        agent.SetDestination(nextAction.target.transform.position);

        if (Vector3.Distance(transform.position, nextAction.target.transform.position) <= 7f)
        {
            // we are at the target location, we are done
            nextAction.setInRange(true);
            return true;
        }
        else
            return false;
    }

    public void planAborted(GoapAction aborter)
    {
        Debug.Log("<color=red>Plan Aborted</color> " + GoapAgent.prettyPrint(aborter));
    }

    public void planFailed(HashSet<KeyValuePair<string, object>> failedGoal)
    {
        Debug.Log("<color=red>Plan Failed</color> " + GoapAgent.prettyPrint(failedGoal));
    }

    public void planFound(HashSet<KeyValuePair<string, object>> goal, Queue<GoapAction> actions)
    {
        Debug.Log("<color=green>Plan found</color> " + GoapAgent.prettyPrint(actions));
    }
}
