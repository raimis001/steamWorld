﻿using UnityEngine;
using System.Collections.Generic;

public class StateMessager
{
    private Queue<IGoalMessage> messageQueue;

    public StateMessager()
    {
        messageQueue = new Queue<IGoalMessage>();
    }

    public void EnqueMessage(IGoalMessage message)
    {
        messageQueue.Enqueue(message);
    }

    /// <summary>Oldest message in queue or null if queue is empty.</summary>
    public IGoalMessage DequeuMessage()
    {
        IGoalMessage message;
        if(messageQueue.Count > 0)
        {
            message = messageQueue.Dequeue();
        }
        else
        {
            message = null;
        }

        return message;
    }
}
