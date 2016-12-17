using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu (menuName = "AI/Create agent")]
public class Agent : ScriptableObject
{
    public float wanderInterval;
    public float attackDistance;
    public bool isDangerous;
    public float distanceToStartFlee;
    public List<string> tagsToFlee;
    public bool isLeader;
}
