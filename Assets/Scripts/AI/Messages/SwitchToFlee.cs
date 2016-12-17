using UnityEngine;
using System.Collections;
using System;

public class SwitchToFlee : IStateMessage
{

    public EStateMessageType GetStateMessageType()
    {
        return EStateMessageType.SWITCH_TO_FLEE;
    }
}
