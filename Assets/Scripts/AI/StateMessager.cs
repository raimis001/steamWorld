using UnityEngine;
using System.Collections.Generic;

public class StateMessager
{
    private Queue<IStateMessage> messageQueue;

    public StateMessager()
    {
        messageQueue = new Queue<IStateMessage>();
    }

    public void EnqueMessage(IStateMessage message)
    {
        messageQueue.Enqueue(message);
    }

    /// <summary>Oldest message in queue or null if queue is empty.</summary>
    public IStateMessage DequeuMessage()
    {
        IStateMessage message;
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
