using UnityEngine;
using System.Collections.Generic;
using System;
using UnityEngine.AI;

public class WanderGoal : IGoal
{
    private NavMeshAgent agent;
    private AgentConfig agentConfig;
    private EGoalState curGoalState;
    private Queue<IGoal> subGoalQeue;
    private IGoal curGoal;

    public WanderGoal(NavMeshAgent agent, AgentConfig agentConfig)
    {
        this.agent = agent;
        this.agentConfig = agentConfig;
        subGoalQeue = new Queue<IGoal>();
        curGoalState = EGoalState.Waiting;
    }

    private Vector3 GetRandomDestination(Vector3 orgin, float radius, int layerMask)
    {
        Vector3 destination = UnityEngine.Random.insideUnitSphere * radius;
        NavMeshHit navHit;
        NavMesh.SamplePosition(orgin + destination, out navHit, radius, layerMask);
        return navHit.position;
    }

    public void Activate()
    {
        // Add sub goals
        AddSubGoal(new MoveToPositionGoal(agent, 
            GetRandomDestination(agentConfig.HomeBasePosition,
           agentConfig.ActivationRadius, -1)));
        AddSubGoal(new IdleGoal(agent, agentConfig.IdleInterval));
        AddSubGoal(new MoveToPositionGoal(agent,
            GetRandomDestination(agentConfig.HomeBasePosition,
           agentConfig.ActivationRadius, -1)));

        // Start working
        curGoalState = EGoalState.InProgress;
    }

    public EGoalState Process()
    {
        if(curGoalState == EGoalState.Waiting)
        {
            Activate();
        }
        Utils.ProcessSubGoals(ref subGoalQeue, ref curGoal, ref curGoalState);

        return curGoalState;
    }

    public bool HandleMessage(IGoalMessage message)
    {
        return false;
    }

    public void AddSubGoal(IGoal goal)
    {
        subGoalQeue.Enqueue(goal);
    }

    public EGoalState GetGoalState()
    {
        return curGoalState;
    }

    public EGoalType GetGoalType()
    {
        return EGoalType.Complex;
    }

    public float GetDesirability()
    {
        return 0;
    }
}
