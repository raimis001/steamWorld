using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FallowGoal : IGoal
{
    private NavMeshAgent agent;
    private GameObject fallowTarget;
    private float fallowRange;
    private EGoalState curState;

    public FallowGoal(NavMeshAgent agent, GameObject fallowTarget, float fallowRange)
    {
        this.fallowTarget = fallowTarget;
        this.fallowRange = fallowRange/2;
        this.agent = agent;
    }

    public void Activate()
    {
        agent.SetDestination(fallowTarget.transform.position);
        curState = EGoalState.InProgress;
    }

    public EGoalState Process()
    {
        if(curState != EGoalState.Finished)
        {
            if ((agent.transform.position - fallowTarget.transform.position).magnitude < fallowRange)
            {
                curState = EGoalState.Finished;
                // Current position is the destination
                agent.SetDestination(agent.transform.position);
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
        Debug.LogError("Follow goal is no sub goals !");
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
