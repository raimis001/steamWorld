using System.Collections;
using System.Collections.Generic;
using UnityEditor.MemoryProfiler;
using UnityEngine;
using UnityEngine.AI;


public class DrinkGoal : IGoal
{
    private NavMeshAgent agent;
    private Vector3 waterPosition;
    private EGoalState curGoalState;
    private Queue<IGoal> subGoals;
    private IGoal curSubGoal;
    private AIManager aiManager;

    public DrinkGoal(AIManager aiManager)
    {
        this.aiManager = aiManager;
        subGoals = new Queue<IGoal>();
    }

    public void Activate()
    {
        // Find drinkable source and move to its location
        string[] drinkSourceTags = aiManager.AgentConfig.DrinkableSourceTags;
        if (drinkSourceTags.Length > 0)
        {
            int index = 0;
            GameObject water = GameObject.FindGameObjectWithTag(drinkSourceTags[index]);
            while (water == null && index < drinkSourceTags.Length)
            {
                water = GameObject.FindGameObjectWithTag(drinkSourceTags[index]);
                index++;
            }

            if (water == null)
            {
                curGoalState = EGoalState.Finished;
            }
            else
            {
                curGoalState = EGoalState.InProgress;
                subGoals.Enqueue(new MoveToPositionGoal(aiManager.NavMeshAgent, water.transform.position));
                subGoals.Enqueue(new IdleGoal(aiManager.NavMeshAgent, aiManager.GoalMessager,
                    aiManager.AgentConfig.DrinkTime));
            }
        }

        Debug.Log("DrinkGoal activated !!!!");
    }

    public EGoalState Process()
    {
        Utils.ProcessSubGoals(ref subGoals, ref curSubGoal, ref curGoalState);

        // We have drunk some water
        if (curGoalState.Equals(EGoalState.Finished))
        {
            aiManager.BeingStats.Thirst = 0;
        }

        return curGoalState;
    }

    public bool HandleMessage(IGoalMessage message)
    {
        return curSubGoal.HandleMessage(message);
    }

    public void AddSubGoal(IGoal goal)
    {
        subGoals.Enqueue(goal);
    }

    public float GetDesirability()
    {
        return aiManager.BeingStats.Thirst;
    }

    public EGoalState GetGoalState()
    {
        return curGoalState;
    }

    public EGoalType GetGoalType()
    {
       return EGoalType.Complex;
    }
}
