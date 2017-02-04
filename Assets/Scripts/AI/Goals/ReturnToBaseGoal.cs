using UnityEngine;
using System.Collections;
using System;
using UnityEngine.AI;

public class ReturnToBaseGoal : IGoal
{
    private Vector3 homeBase;
    private NavMeshAgent agent;
    private EGoalState curState;

    // TODO: Return to base goal either should be reduced to move to position goal or become
   // a complex goal that has move to position goal as sub goal.
    public ReturnToBaseGoal(NavMeshAgent agent, Vector3 homeBase)
    {
        this.agent = agent;
        this.homeBase = homeBase;
    }

    public void Activate()
    {
        agent.SetDestination(homeBase);
        curState = EGoalState.InProgress;
    }

    public EGoalState Process()
    {
        if (agent.remainingDistance == 0)
        {
            curState = EGoalState.Finished;
        }

        return curState;
    }

    public bool HandleMessage(IGoalMessage message)
    {
        // Message not handled
        return false;
    }

    public void AddSubGoal(IGoal goal)
    {
        Debug.LogError("Return to base goal has no sub goals!");
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
