using UnityEngine;
using System.Collections;
using System;

public class SwitchToAttack : IStateMessage
{
    private GameObject targetObject;

    public SwitchToAttack(GameObject targetObject)
    {
        this.targetObject = targetObject;
    }

    public EStateMessageType GetStateMessageType()
    {
        return EStateMessageType.SWITCH_TO_ATTACK;
    }

    public GameObject TargetObject
    {
        get { return targetObject; }
    }
}
