using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils
{
    public static void ProcessSubGoals(ref Queue<IGoal> goalQueue, ref IGoal curGoal,
        ref EGoalState curState)
    {
        if (goalQueue.Count == 0)
        {
            curState = EGoalState.Finished;
        }
        else
        {
            if (curGoal == null || curGoal.GetGoalState() == EGoalState.Finished)
            {
                curGoal = goalQueue.Dequeue();
            }
            else
            {
                curGoal.Process();
            }
        }
    }
}
