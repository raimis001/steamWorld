using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu (menuName = "AI/Create agent config")]
public class AgentConfig : ScriptableObject
{
    // Default behaviour

    /* Init values */
    private Vector2 homeBasePosition;
    private float activationRadius;
    private GameObject target;

    /* User defined values */
    [SerializeField]
    private float idleInterval;
    [SerializeField]
    private float wanderInterval;

    // Attack behaviour
    [SerializeField]
    private bool isDangerous;
    [SerializeField]
    private float attackDistance;
    [SerializeField]
    private List<string> tagsToAttack;

    // Flee behaviour
    [SerializeField]
    private float distanceToStartFlee;
    [SerializeField]
    private List<string> tagsToFlee;

    // Follow behaviour
    [SerializeField]
    private bool shouldFollow;
    [SerializeField]
    private string followName;
    [SerializeField]
    private float followRange;

    // Water search behaviour
    [SerializeField] private string[] tagsToConsiderDrinkable;
    [SerializeField] private float drinkingTime;

    // Sleep behaviour
    [SerializeField] private string[] tagsToConsiderSleepable;
    [SerializeField] private float sleepTime;
   


    public float WanderInterval
    {
        get { return wanderInterval; }
    }
    public bool IsDangerous
    {
        get { return isDangerous; }
    }
    public float AttackDistance
    {
        get { return attackDistance; }
    }
    public List<string> TagsToAttack
    {
        get { return tagsToAttack; }
    }
    public List<string> TagsToFlee
    {
        get { return tagsToFlee; }
    }
    public bool ShouldFollow
    {
        get { return shouldFollow; }
        set { shouldFollow = value; }
    }
    public string FollowName
    {
        get { return followName; }
        set { followName = value; }
    }
    public float FollowRange
    {
        get { return followRange; }
        set { followRange = value; }
    }
    public float DistanceToStartFlee
    {
        get { return distanceToStartFlee; }
    }
    public float IdleInterval
    {
        get { return idleInterval; }
    }
    public Vector2 HomeBasePosition
    {
        get { return homeBasePosition; }
        set { homeBasePosition = value; }
    }

    public float ActivationRadius
    {
        get { return activationRadius; }
        set { activationRadius = value; }
    }

    public GameObject Target
    {
        get { return target; }
        set { target = value; }
    }

    public float DrinkTime
    {
        get { return drinkingTime; }
        set { drinkingTime = value; }
    }

    public string[] DrinkableSourceTags
    {
        get { return tagsToConsiderDrinkable; }        
    }

    public string[] SleepablePlaces
    {
        get { return tagsToConsiderSleepable; }
    }

    public float SleepTime
    {
        get { return sleepTime; }
    }
}
