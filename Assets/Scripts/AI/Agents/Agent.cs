using UnityEngine;
using System.Collections;

[CreateAssetMenu (menuName = "AI/Create agent")]
public class Agent : ScriptableObject
{
    public float wanderInterval;
    public float attackDistance;
}
