using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchToFallow : IStateMessage
{
    public EStateMessageType GetStateMessageType()
    {
          return EStateMessageType.SWITCH_TO_FALLOW;
    }
}
