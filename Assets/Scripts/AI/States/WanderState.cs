using UnityEngine;
using System.Collections;
using System;

public class WanderState: IState
{
    private NavMeshAgent agent;
    private float wanderRadius;
    private float wanderInterval;
    private Vector3 homeBaseInWorldSpace;

    public WanderState(NavMeshAgent agent, Vector3 homeBaseInWorldSpace, float wanderRadius,
        float wanderInterval)
    {
        this.agent = agent;
        this.wanderRadius = wanderRadius;
        this.wanderInterval = wanderInterval;
        this.homeBaseInWorldSpace = homeBaseInWorldSpace;
    }

    public void OnStateEnter()
    {
        agent.SetDestination(GetRandomDestination(homeBaseInWorldSpace,
            wanderRadius, -1));
    }

    public void Update(StateMessager messager)
    {
        if(agent.remainingDistance == 0 && agent.speed > 0)
        {
            messager.EnqueMessage(new SwitchToIdle());
        }
    }

    public void OnStateExit()
    {
    }

    private Vector3 GetRandomDestination(Vector3 orgin, float radius, int layerMask)
    {
        Vector3 destination = UnityEngine.Random.insideUnitSphere * radius;
        NavMeshHit navHit;
        NavMesh.SamplePosition(orgin + destination, out navHit, radius, layerMask);
        return navHit.position;
    }
}
