using UnityEngine;
using System.Collections;
using System;

public class IdleState : IState
{
    private NavMeshAgent agent;
    private float idleTime;
    private float curTime;
    private float curRotation;

    public IdleState(NavMeshAgent agent, float idleTime)
    {
        this.agent = agent;
        this.idleTime = idleTime;
        curTime = 0;
    }

    public void OnStateEnter()
    {
    }

    public void Update(StateMessager messager)
    {
        curTime += Time.deltaTime;
        if(curTime > idleTime)
        {
            messager.EnqueMessage(new SwitchToWander());
            curTime = 0;
        }
        curRotation += Time.deltaTime;
        agent.transform.rotation = Quaternion.Euler(0, Mathf.Sin(curRotation) * 50, 0);
    }

    public void OnStateExit()
    {
    }

}
