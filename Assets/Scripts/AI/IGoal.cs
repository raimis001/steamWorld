using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGoal
{
    void Activate();
    EGoalState Process();
    bool HandleMessage(IGoalMessage message);
    void AddSubGoal(IGoal goal);
    float GetDesirability();
    EGoalState GetGoalState();
    EGoalType GetGoalType();
}
