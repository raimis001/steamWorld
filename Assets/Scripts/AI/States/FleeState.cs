using UnityEngine;
using System.Collections;
using System;

public class FleeState : IState
{
    private UnityEngine.AI.NavMeshAgent agent;
    private GameObject fleeFrom;
    private float distanceToHold;

    public FleeState(UnityEngine.AI.NavMeshAgent agent, GameObject fleeFrom, float distanceToHold)
    {
        this.agent = agent;
        this.fleeFrom = fleeFrom;
        this.distanceToHold = distanceToHold;
    }

    public void OnStateEnter()
    {
       
    }

    public void Update(StateMessager messager)
    {
        Vector3 toAgent = agent.transform.position - fleeFrom.transform.position;
        float distance = toAgent.magnitude;
        if(distance > distanceToHold)
        {
            messager.EnqueMessage(new SwitchToIdle());
        }
        else
        {
            agent.SetDestination(agent.transform.position + toAgent);
        }
    }

    public void OnStateExit()
    {
       
    }

    public string GetName()
    {
        return typeof(FleeState).Name;
    }
}
