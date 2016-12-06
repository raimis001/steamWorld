using UnityEngine;
using System.Collections;
using System;

public class SwitchToReturn : IStateMessage
{
    public EStateMessageType GetStateMessageType()
    {
        return EStateMessageType.SWITCH_TO_RETURN;
    }
}
