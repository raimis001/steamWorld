using UnityEngine;
using System.Collections;
using UnityEngine.Assertions;
using UnityEngine.AI;

public class AIManager : MonoBehaviour
{
    [SerializeField]
    private AgentConfig agentConfig;
    private NavMeshAgent agent;
    private GoalMessager messager;
    private IGoal thinkGoal;

    private Being beingStats;

    void Awake()
    {
        // Get being stats
        beingStats = GetComponent<Being>();
        Assert.IsNotNull(beingStats, "Every AI agent should have a 'Being' script attached to it !!");

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

        messager = new GoalMessager();
        thinkGoal = new ThinkGoal(this);
    }

    void Update()
    {
        
        FleeCheck();
        //FallowCheck();

        IGoalMessage message = messager.DequeuMessage();
        if (message != null)
        {
            // Pass the message down the goal chain
            if (thinkGoal.HandleMessage(message))
            {
                // Message handled successfully                
            }
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
            beingStats.Fear += 0.8f;
        }
    }
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
        get { return beingStats.Hunger; }
    }

    public float Fear
    {
        get { return beingStats.Fear; }
    }

    public float Tiredness
    {
        get { return beingStats.Tiredness; }
    }

    public Being BeingStats
    {
        get { return beingStats; }
    }

    public NavMeshAgent NavMeshAgent
    {
        get { return agent; }
    }

    public AgentConfig AgentConfig
    {
        get { return agentConfig; }
    }

    public GoalMessager GoalMessager
    {
        get { return messager; }
    }
}
