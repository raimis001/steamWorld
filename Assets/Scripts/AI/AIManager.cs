using UnityEngine;
using System.Collections;
using UnityEngine.Assertions;
using UnityEngine.AI;

public class AIManager : MonoBehaviour
{
    public AgentConfig agentConfig;
    private NavMeshAgent agent;
    private StateMessager messager;
    private IGoal thinkGoal;

    private float hunger;
    private float fear;
    private float tiredNess;

    void Awake()
    {
        // Get agent instance
        agent = GetComponentInChildren<NavMeshAgent>();
        Assert.IsNotNull(agent, "No NavMeshAgent found in GameObject: " + name);

        // Init agents base working parameters
        agentConfig.HomeBasePosition = transform.position;
        SphereCollider sphere = GetComponent<SphereCollider>();
        Assert.IsNotNull(sphere, "No sphere colider found! GameObject:" +
            name);
        agentConfig.ActivationRadius = sphere.radius;

        // Find follow game object
        if (agentConfig.ShouldFollow)
        {
            Transform followTransf = transform.parent.Find(agentConfig.FollowName);
            Assert.IsNotNull(followTransf, "" + agentConfig.name + " is trying to follow " +
                agentConfig.FollowName + " but cant find it in local hiarchy!");
            agentConfig.Target = followTransf.gameObject;
        }

        messager = new StateMessager();
        thinkGoal = new ThinkGoal(this);
        fear = 0;
        tiredNess = 0;
        hunger = 0;
    }

    void Update()
    {
       /* 
        FleeCheck();
        FallowCheck();*/

        IGoalMessage message = messager.DequeuMessage();
        if(message !=null)
        {
            // pass the message down the goal chain
            thinkGoal.HandleMessage(message);
        }
        thinkGoal.Process();
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
           
        }
    }
   /* private void FleeCheck()
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
    }*/
    /*private void FallowCheck()
    {
        if(followTarget != null)
        {
            // if fallow target is further away than allowed then fallow it
            if((agent.transform.position-followTarget.transform.position).magnitude > agentConfig.FollowRange)
            {
                messager.EnqueMessage(new SwitchToFallow());
            }
        }
    }*/

    public float Hunger
    {
        get { return hunger; }
    }

    public float Fear
    {
        get { return fear; }
    }

    public float Tiredness
    {
        get { return tiredNess; }
    }

    public NavMeshAgent NavMeshAgent
    {
        get { return agent; }
    }
}
