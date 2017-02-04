using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveToPositionGoal : IGoal
{
    private EGoalState curState;
    private NavMeshAgent agent;
    private Vector3 targetPosition;

    public MoveToPositionGoal(NavMeshAgent agent, Vector3 targetPosition)
    {
        this.agent = agent;
        this.targetPosition = targetPosition;
        curState = EGoalState.Waiting;
    }

    public void Activate()
    {
        curState = EGoalState.InProgress;
        agent.SetDestination(targetPosition);
    }

    public EGoalState Process()
    {
        if (curState == EGoalState.Waiting)
        {
            Activate();
        }

        if(agent.remainingDistance < 0.5f)
        {
            curState = EGoalState.Finished;
        }

        return curState;    
    }

    public void AddSubGoal(IGoal goal)
    {
        Debug.LogError("Move To Position is simple goal and does not have sub goals !");
    }

    public float GetDesirability()
    {
        return 0;
    }

    public EGoalState GetGoalState()
    {
        return curState;
    }

    public EGoalType GetGoalType()
    {
        return EGoalType.Simple;
    }

    public bool HandleMessage(IGoalMessage message)
    {
        return false;
    }

}
