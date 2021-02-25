using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventManager : MonoBehaviour
{
    public readonly Stack<GameEvent> Events = new Stack<GameEvent>();

    public bool IsEmpty()
    {
        while(Events.Count > 0 && Events.Peek().IsFinished())
            PlayEvent();

        return Events.Count == 0;
    }

    public void Push(GameEvent gameEvent)
    {
        Events.Push(gameEvent);
    }

    public void Wait(float Timer)
    {
        Events.Peek().Wait(Timer);
    }

    private void PlayEvent()
    {
        //TODO Allow external forces to prevent event trigger

        GameEvent gameEvent = Events.Pop();
        gameEvent.Trigger();
    }

    public void Pop()
    {
        Events.Pop();
    }

    public void Pop(string name)
    {
        if (Events.Peek().GetName() == name)
            Events.Pop();
    }

    public string GetActiveEvent()
    {
        if (IsEmpty())
            return "";

        return Events.Peek().GetName();
    }
}
