using UnityEngine;
using System.Collections;
using System;
using UnityEngine.AI;

public class ReturnToBase : IState
{
    private Vector3 homeBase;
    private NavMeshAgent agent;

    public ReturnToBase(NavMeshAgent agent, Vector3 homeBase)
    {
        this.agent = agent;
        this.homeBase = homeBase;
    }

    public void OnStateEnter()
    {
        agent.SetDestination(homeBase);
    }

    public void OnStateExit()
    {
    }

    public void Update(StateMessager messager)
    {
        if(agent.remainingDistance == 0)
        {
            messager.EnqueMessage(new SwitchToWander());
        }
    }

    public string GetName()
    {
        return typeof(ReturnToBase).Name;
    }
}
