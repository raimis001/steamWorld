using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThinkGoal : IGoal
{
    private IGoal curGoal;
    private List<IGoal> possibleGoals = new List<IGoal>();
    private EGoalState curGoalState;
    private AIManager aiManager;

    public ThinkGoal(AIManager aiManager)
    {
        this.aiManager = aiManager;
        curGoalState = EGoalState.Waiting;
    }

    public void Activate()
    {
        curGoalState = EGoalState.InProgress;

        // Here comes all goals to consider
        possibleGoals.Add(new SleepGoal(aiManager));
        possibleGoals.Add(new ReproduceGoal(aiManager));
        possibleGoals.Add(new EatGoal(aiManager));
        possibleGoals.Add(new DrinkGoal(aiManager));

        Debug.Log("Starting to think !!!");
    }

    public EGoalState Process()
    {
        if(curGoalState == EGoalState.Waiting)
        {
            Activate();
        }

        if(curGoal == null || curGoal.GetGoalState() == EGoalState.Finished)
        {        
            float curDesirability = 0;
            float bestdesirability = 0;

            // Go through all high level goal options and choose the most desirable
            foreach (IGoal goal in possibleGoals)
            {
                curDesirability = goal.GetDesirability();
                if (curDesirability > bestdesirability)
                {
                    bestdesirability = curDesirability;
                    curGoal = goal;
                }
            }
            curGoal.Activate();
        }

        curGoal.Process();
        

        return curGoalState;
    }

    public void AddSubGoal(IGoal goal)
    {
        possibleGoals.Add(goal);
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

    public float GetDesirability()
    {
        return 1;
    }
}
