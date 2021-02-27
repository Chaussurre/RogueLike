using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventManager : MonoBehaviour
{
    public readonly Stack<GameEvent> Events = new Stack<GameEvent>();
    public readonly HashSet<GameEventWatcher> Watchers = new HashSet<GameEventWatcher>();

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
        GameEvent gameEvent = Events.Pop();

        foreach (GameEventWatcher watcher in Watchers)
            if (watcher.Check(gameEvent) && !watcher.IsAllowed(gameEvent))
                return;
        
        gameEvent.Trigger();

        foreach (GameEventWatcher watcher in Watchers)
            if (watcher.Check(gameEvent))
                watcher.OnTrigger(gameEvent);
    }

    public void Pop()
    {
        Events.Pop();
    }

    public void Pop(System.Type type)
    {
        if (Events.Peek().GetType() == type)
            Events.Pop();
    }

    public System.Type GetActiveEvent()
    {
        if (IsEmpty())
            return null;

        return Events.Peek().GetType();
    }

    public void AddWatcher(GameEventWatcher watcher)
    {
        Watchers.Add(watcher);
    }

    public void RemoveWatcher(GameEventWatcher watcher)
    {
        Watchers.Remove(watcher);
    }
}
