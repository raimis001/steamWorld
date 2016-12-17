using UnityEngine;
using System.Collections;
using UnityEngine.Assertions;

public class AIStateMachine : MonoBehaviour
{
    public Agent curAgent;
    private IState curState;
    private UnityEngine.AI.NavMeshAgent agent;
    private StateMessager messager;
    private Vector3 agentHomeBase;
    private float workingRadius;

    void Start()
    {
        agent = GetComponentInChildren<UnityEngine.AI.NavMeshAgent>();
        Assert.IsNotNull(agent, "No NavMeshAgent found in GameObject: " + name);
        agentHomeBase = transform.position;
        messager = new StateMessager();
        SphereCollider sphere = GetComponent<SphereCollider>();
        Assert.IsNotNull(sphere, "No sphere colider found! GameObject:" +
            name);
        workingRadius = sphere.radius;

        // Init starting state
        SwitchState(new WanderState(agent, agentHomeBase, workingRadius, curAgent.wanderInterval));
    }
    void Update()
    {
        curState.Update(messager);
        IStateMessage msg = messager.DequeuMessage();
        if (msg != null)
        {
            switch (msg.GetStateMessageType())
            {
                case EStateMessageType.SWITCH_TO_IDLE:
                    SwitchState(new IdleState(agent, 10));
                    break;

                case EStateMessageType.SWITCH_TO_WANDER:
                    SwitchState(new WanderState(agent, agentHomeBase,
                        workingRadius, curAgent.wanderInterval));
                    break;

                case EStateMessageType.SWITCH_TO_ATTACK:
                    SwitchToAttack attackMsg = msg as SwitchToAttack;
                    SwitchState(new AttackState(agent, attackMsg.TargetObject,
                        curAgent.attackDistance));
                    break;

                case EStateMessageType.SWITCH_TO_RETURN:
                    SwitchState(new ReturnToBase(agent, agentHomeBase));
                    break;
            }
        }

        // Flee check
        var coliders = Physics.OverlapSphere(agent.transform.position, curAgent.distanceToStartFlee);
        float closest = curAgent.distanceToStartFlee+1;
        Collider closestCollider = null;
        foreach (Collider colli in coliders)
        {
            if(curAgent.tagsToFlee.Exists(it => it == colli.tag))
            {
                float curSqrtDistance = (colli.gameObject.transform.position
                - agent.transform.position).sqrMagnitude;
                if (curSqrtDistance < closest)
                {
                    closest = curSqrtDistance;
                    closestCollider = colli;
                }
            }
        }
        if(closestCollider != null)
        {
            SwitchState(new FleeState(agent, closestCollider.gameObject,
                curAgent.distanceToStartFlee));
        }
    }

    public void SwitchState(IState newState)
    {
        if (curState != null)
        {
            curState.OnStateExit();
        }
        curState = newState;
        newState.OnStateEnter();
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            messager.EnqueMessage(new SwitchToAttack(other.gameObject));
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            messager.EnqueMessage(new SwitchToReturn());
        }
    }
}
