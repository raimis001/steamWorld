using UnityEngine;
using System.Collections;
using System;

public class SwitchToIdle : IStateMessage
{
    public EStateMessageType GetStateMessageType()
    {
        return EStateMessageType.SWITCH_TO_IDLE;
    }
}
