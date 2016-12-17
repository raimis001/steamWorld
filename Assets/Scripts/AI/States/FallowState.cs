using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FallowState : IState
{
    private NavMeshAgent agent;
    private GameObject fallowTarget;
    private float fallowRange;

    public FallowState(NavMeshAgent agent, GameObject fallowTarget, float fallowRange)
    {
        this.fallowTarget = fallowTarget;
        this.fallowRange = fallowRange/2;
        this.agent = agent;
    }

    public void OnStateEnter()
    {
        agent.SetDestination(fallowTarget.transform.position);
    }

    public void Update(StateMessager messager)
    {
        if((agent.transform.position-fallowTarget.transform.position).magnitude < fallowRange)
        {
            messager.EnqueMessage(new SwitchToIdle());
        }
    }

    public void OnStateExit()
    {
        // Current position is the destination
        agent.SetDestination(agent.transform.position);
    }

    public string GetName()
    {
        return typeof(FallowState).Name;
    }
}
