using UnityEngine;
using System.Collections;
using UnityEngine.Assertions;
using UnityEngine.AI;

public class AIStateMachine : MonoBehaviour
{
    public Agent agentConfig;
    private IState curState;
    private NavMeshAgent agent;
    private StateMessager messager;

    private GameObject followTarget;
    private Vector3 agentHomeBase;
    private float workingRadius;

    void Awake()
    {
        // Get agent instance
        agent = GetComponentInChildren<NavMeshAgent>();
        Assert.IsNotNull(agent, "No NavMeshAgent found in GameObject: " + name);

        // Init agents base working parameters
        agentHomeBase = transform.position;
        SphereCollider sphere = GetComponent<SphereCollider>();
        Assert.IsNotNull(sphere, "No sphere colider found! GameObject:" +
            name);
        workingRadius = sphere.radius;

        // Find follow game object
        if (agentConfig.ShouldFollow)
        {
            Transform followTransf = transform.parent.Find(agentConfig.FollowName);
            Assert.IsNotNull(followTransf, "" + agentConfig.name + " is trying to follow " +
                agentConfig.FollowName + " but cant find it in local hiarchy!");
            followTarget = followTransf.gameObject;
        }

        // Init messager and starting state
        messager = new StateMessager();
        SwitchState(new WanderState(agent, agentHomeBase, workingRadius, agentConfig.WanderInterval));
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
                    SwitchState(new IdleState(agent, agentConfig.IdleInterval));
                    break;

                case EStateMessageType.SWITCH_TO_WANDER:
                    if(agentConfig.ShouldFollow)
                    {
                        SwitchState(new WanderState(agent, followTarget.transform.position,
                            agentConfig.FollowRange, agentConfig.WanderInterval));
                    }
                    else
                    {
                        SwitchState(new WanderState(agent, agentHomeBase,
                            workingRadius, agentConfig.WanderInterval));
                    }
                    break;

                case EStateMessageType.SWITCH_TO_ATTACK:
                    SwitchToAttack attackMsg = msg as SwitchToAttack;
                    SwitchState(new AttackState(agent, attackMsg.TargetObject,
                        agentConfig.AttackDistance));
                    break;

                case EStateMessageType.SWITCH_TO_RETURN:
                    SwitchState(new ReturnToBase(agent, agentHomeBase));
                    break;

                case EStateMessageType.SWITCH_TO_FALLOW:
                    if(agentConfig.ShouldFollow)
                    {
                        if (followTarget != null)
                        {
                            SwitchState(new FallowState(agent, followTarget,
                                agentConfig.FollowRange));
                        }
                    }
                    else
                    {
                        Debug.LogError("" + agentConfig.name + " Received fallow message even,"+
                                " but its not marked as [shoulFallow] !");
                    }
                    break;
            }
        }

        FleeCheck();
        FallowCheck();
    }

    public void SwitchState(IState newState)
    {
        if (curState != null)
        {
            Debug.Log("Agent: " + agentConfig.name + " state changed from: " +
                curState.GetName() + " to: " + newState.GetName());

            curState.OnStateExit();
        }
        curState = newState;
        newState.OnStateEnter();
    }

    public void OnTriggerEnter(Collider other)
    {
        TryAttack(other);
    }

    public void OnTriggerExit(Collider other)
    {
        TryAttack(other);
    }

    private void TryAttack(Collider other)
    {
        if (agentConfig.TagsToAttack.Contains(other.gameObject.tag))
        {
            messager.EnqueMessage(new SwitchToAttack(other.gameObject));
        }
    }
    private void FleeCheck()
    {
        var coliders = Physics.OverlapSphere(agent.transform.position, agentConfig.DistanceToStartFlee);
        float closest = agentConfig.DistanceToStartFlee + 1;
        Collider closestCollider = null;
        foreach (Collider colli in coliders)
        {
            if (agentConfig.TagsToFlee.Contains(colli.tag))
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
        if (closestCollider != null)
        {
            SwitchState(new FleeState(agent, closestCollider.gameObject,
                agentConfig.DistanceToStartFlee));
        }
    }
    private void FallowCheck()
    {
        if(followTarget != null)
        {
            // if fallow target is further away than allowed then fallow it
            if((agent.transform.position-followTarget.transform.position).magnitude > agentConfig.FollowRange)
            {
                messager.EnqueMessage(new SwitchToFallow());
            }
        }
    }
}
