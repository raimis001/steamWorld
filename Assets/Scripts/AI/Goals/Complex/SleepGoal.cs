using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleepGoal : IGoal
{
    private EGoalState curGoalState;
    private Queue<IGoal> subGoals = new Queue<IGoal>();
    private AIManager aiManager;
    private IGoal curGoal;

    public SleepGoal(AIManager aiManager)
    {
        this.aiManager = aiManager;
    }

    public void Activate()
    {
        // Find drinkable source and move to its location
        string[] sleepPlaces = aiManager.AgentConfig.SleepablePlaces;
        if (sleepPlaces.Length > 0)
        {
            int index = 0;
            GameObject bed = GameObject.FindGameObjectWithTag(sleepPlaces[index]);
            while (bed == null && index < sleepPlaces.Length)
            {
                bed = GameObject.FindGameObjectWithTag(sleepPlaces[index]);
                index++;
            }

            if (bed == null)
            {
                curGoalState = EGoalState.Finished;
            }
            else
            {
                // Setting the game plan
                curGoalState = EGoalState.InProgress;

                // First find place to sleep in
                subGoals.Enqueue(new MoveToPositionGoal(aiManager.NavMeshAgent, bed.transform.position));

                // Then close your eyes and ZzzzzzZZZZzzzzZZ take a snooze
                subGoals.Enqueue(new IdleGoal(aiManager.NavMeshAgent, aiManager.GoalMessager,
                    aiManager.AgentConfig.SleepTime));
            }
        }

        Debug.Log("Sleep goal activated !!!!");

    }

    public EGoalState Process()
    {
        Utils.ProcessSubGoals(ref subGoals, ref curGoal, ref curGoalState);

        // We have taken a nice nap
        if (curGoalState.Equals(EGoalState.Finished))
        {
            // Usually after a nice nap you are not tired and do not feel fearful
            aiManager.BeingStats.Fear = 0;
            aiManager.BeingStats.Tiredness = 0;
        }

        return curGoalState;
    }

    public void AddSubGoal(IGoal goal)
    {
        subGoals.Enqueue(goal);
    }

    public float GetDesirability()
    {
        return (aiManager.Tiredness + (1 - (aiManager.Hunger * 0.4f))) / 2f;
    }

    public EGoalState GetGoalState()
    {
        return curGoalState;
    }

    public EGoalType GetGoalType()
    {
        return EGoalType.Complex;
    }

    public bool HandleMessage(IGoalMessage message)
    {
        return curGoal.HandleMessage(message);
    }
}
