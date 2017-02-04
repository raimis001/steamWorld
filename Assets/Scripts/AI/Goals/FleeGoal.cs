using UnityEngine;
using System.Collections;
using System;

public class FleeGoal : IGoal
{
    private UnityEngine.AI.NavMeshAgent agent;
    private GameObject fleeFrom;
    private float distanceToHold;
    private EGoalState curState;

    public FleeGoal(UnityEngine.AI.NavMeshAgent agent, GameObject fleeFrom, float distanceToHold)
    {
        this.agent = agent;
        this.fleeFrom = fleeFrom;
        this.distanceToHold = distanceToHold;
    }

    public void Activate()
    {
        curState = EGoalState.InProgress;
    }

    public EGoalState Process()
    {
       if(curState != EGoalState.Finished)
        {
            Vector3 toAgent = agent.transform.position - fleeFrom.transform.position;
            float distance = toAgent.magnitude;
            if (distance > distanceToHold)
            {
                curState = EGoalState.Finished;
            }
            else
            {
                agent.SetDestination(agent.transform.position + toAgent);
            }
        }

        return curState;
    }

    public bool HandleMessage(IGoalMessage message)
    {
        return false;
    }

    public void AddSubGoal(IGoal goal)
    {
        Debug.LogError("Flee goal has no sub goals !");
    }

    public EGoalState GetGoalState()
    {
        return curState;
    }

    public EGoalType GetGoalType()
    {
        return EGoalType.Simple;
    }

    public float GetDesirability()
    {
        return 0;
    }
}
