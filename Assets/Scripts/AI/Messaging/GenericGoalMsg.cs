using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericGoalMsg : IGoalMessage
{
    private Type senderType;

    public GenericGoalMsg(Type senderType)
    {
        this.senderType = senderType;
    }

    public Type GetSenderType()
    {
        return senderType;
    }
}
