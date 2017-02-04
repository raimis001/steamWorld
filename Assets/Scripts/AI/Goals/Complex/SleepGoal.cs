using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleepGoal : IGoal
{
    private EGoalState curGoalState;
    private List<IGoal> subGoalList = new List<IGoal>();
    private AIManager aiManager;

    public SleepGoal(AIManager aiManager)
    {
        this.aiManager = aiManager;
    }

    public void Activate()
    {
        curGoalState = EGoalState.InProgress;    
    }

    public EGoalState Process()
    {
        if(subGoalList.TrueForAll(it=>it.GetGoalState() == EGoalState.Finished))
        {
            curGoalState = EGoalState.Finished;
        }

        return curGoalState;
    }

    public void AddSubGoal(IGoal goal)
    {
        subGoalList.Add(goal);
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
        return false;
    }
}
