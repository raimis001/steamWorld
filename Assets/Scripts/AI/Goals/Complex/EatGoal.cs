using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatGoal : IGoal
{
    private EGoalState curState;
    private List<IGoal> subGoalList = new List<IGoal>();
    private AIManager aiManager;

    public EatGoal(AIManager aiManager)
    {
        this.aiManager = aiManager;
    }

    public void Activate()
    {
        curState = EGoalState.InProgress;
    }

    public EGoalState Process()
    {
        if(subGoalList.TrueForAll(it => it.GetGoalState() == EGoalState.Finished))
        {
            curState = EGoalState.Finished;
        }

        return curState;    
    }

    public void AddSubGoal(IGoal goal)
    {
        subGoalList.Add(goal);
    }

    public float GetDesirability()
    {
        return (aiManager.Hunger + (1 - (aiManager.Tiredness * 0.4f))) / 2f;
    }

    public EGoalState GetGoalState()
    {
        return curState;
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
