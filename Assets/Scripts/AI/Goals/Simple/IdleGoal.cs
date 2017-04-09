using UnityEngine;
using System.Collections;
using System;
using UnityEngine.AI;

public class IdleGoal : IGoal
{
    private GoalMessager messager;
    private EGoalState curState;
    private NavMeshAgent agent;
    private float idleTime;
    private float curTime;
    private float curRotation;

    public IdleGoal(NavMeshAgent agent, GoalMessager messager, float idleTime)
    {
        this.agent = agent;
        this.messager = messager;
        this.idleTime = idleTime;

        curTime = 0;
        curRotation = 0;
        curState = EGoalState.Waiting;
    }

    public void Activate()
    {
        curState = EGoalState.InProgress;

        Debug.Log("Idle goal in progress !!!!");
    }

    public EGoalState Process()
    {
        if (curState != EGoalState.Finished)
        {
            curTime += Time.deltaTime;
            if (curTime > idleTime)
            {
                curState = EGoalState.Finished;
                curTime = 0;
                curRotation = 0;
            }

            // Peform rotating action, indicating of Idle state
            curRotation += Time.deltaTime;
            agent.transform.rotation = Quaternion.Euler(0, Mathf.Sin(curRotation) * 50, 0);
        }

        return curState;
    }

    public bool HandleMessage(IGoalMessage message)
    {
        return false;
    }

    public void AddSubGoal(IGoal goal)
    {
        Debug.LogError("Idle goal has no subgoals !");
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
