using UnityEngine;
using System.Collections;
using System;
using UnityEngine.AI;

public class AttackGoal : IGoal
{
    private NavMeshAgent agent;
    private GameObject targetObject;
    private float attackDistance;
    private EGoalState curState;

    public AttackGoal(NavMeshAgent agent, GameObject targetObject,
        float attackDistance)
    {
        this.agent = agent;
        this.targetObject = targetObject;
        this.attackDistance = attackDistance;
    }

    public void Activate()
    {
        curState = EGoalState.InProgress;
    }

    public EGoalState Process()
    {
        float diff = (agent.transform.position -
            targetObject.transform.position).magnitude;
        if (diff > attackDistance)
        {
            // Move closer
            agent.SetDestination(Vector3.Scale(targetObject.transform.position,
                new Vector3(0.98f, 0.98f, 0.98f)));
        }
        else
        {
            // Attack
        }

        return curState;
    }

    public bool HandleMessage(IGoalMessage message)
    {
        return false;
    }

    public void AddSubGoal(IGoal goal)
    {
       // This is a simple goal so it does not have sub goals
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
