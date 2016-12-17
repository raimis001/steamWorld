using UnityEngine;
using System.Collections;
using System;

public class AttackState : IState
{
    private UnityEngine.AI.NavMeshAgent agent;
    private GameObject targetObject;
    private float attackDistance;

    public AttackState(UnityEngine.AI.NavMeshAgent agent, GameObject targetObject, float attackDistance)
    {
        this.agent = agent;
        this.targetObject = targetObject;
        this.attackDistance = attackDistance;
    }

    public void OnStateEnter()
    {
      
    }

    public void Update(StateMessager messager)
    {
        float diff = (agent.transform.position - 
            targetObject.transform.position).magnitude;
        if(diff > attackDistance)
        {
            // Move closer
            agent.SetDestination(Vector3.Scale(targetObject.transform.position, 
                new Vector3(0.98f, 0.98f, 0.98f)));
        }
        else
        {
            // Attack
        }
    }

    public void OnStateExit()
    {
        
    }
}
