using UnityEngine;
using System.Collections;
using System;

public class SwitchToWander : IStateMessage
{
    public EStateMessageType GetStateMessageType()
    {
        return EStateMessageType.SWITCH_TO_WANDER;
    }
}
