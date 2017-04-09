using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils
{

    // Process all sub goals, if all sub goals are completed then this goal is finished.
    public static void ProcessSubGoals(ref Queue<IGoal> goalQueue, ref IGoal curGoal,
        ref EGoalState curState)
    {
        if (goalQueue.Count == 0 && curGoal.GetGoalState() == EGoalState.Finished)
        {
            curState = EGoalState.Finished;
        }
        else
        {
            if (curGoal == null || curGoal.GetGoalState() == EGoalState.Finished)
            {
                curGoal = goalQueue.Dequeue();
                curGoal.Activate();
            }
            else
            {
                curGoal.Process();
            }
        }        
    }
}
