using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : GoapAction
{
    bool idle;
    public Idle()
    {
        addEffect("idle", true);
    }
    public override bool checkProceduralPrecondition(GameObject agent)
    {
        return true;
    }

    public override bool isDone()
    {
        return idle;
    }

    public override bool perform(GameObject agent)
    {
        gameObject.transform.position = transform.position;
        idle = true;
        return true;
    }

    public override bool requiresInRange()
    {
        return false;
    }

    public override void reset()
    {
        idle = false;
    }
}
