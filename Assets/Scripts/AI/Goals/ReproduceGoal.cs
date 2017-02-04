using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReproduceGoal : IGoal
{
    private EGoalState curState;
    private Queue<IGoal> subGoalQueue = new Queue<IGoal>();
    private AIManager aiManager;
    private IGoal curGoal;

    public ReproduceGoal(AIManager aiManager)
    {
        this.aiManager = aiManager;
        curState = EGoalState.Waiting;
    }

    public void Activate()
    {
        curState = EGoalState.InProgress;
        AddSubGoal(new WanderGoal(aiManager.NavMeshAgent, aiManager.agentConfig));
    }

    public EGoalState Process()
    {
        if(curState == EGoalState.Waiting)
        {
            Activate();
        }
        Utils.ProcessSubGoals(ref subGoalQueue, ref curGoal, ref curState);
        return curState;
    }

    public void AddSubGoal(IGoal goal)
    {
        subGoalQueue.Enqueue(goal);
    }

    public float GetDesirability()
    {
        return 1 - ((aiManager.Hunger + aiManager.Fear + aiManager.Tiredness)/3);
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
