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
        gameEvent.OnStack();
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
        if (Events.Count > 0)
            Events.Pop();
    }

    public void Pop(System.Type type)
    {
        if (Events.Peek().GetType() == type)
            Pop();
    }

    public void Cancel()
    {
        GameEvent e = Events.Pop();
        while (!e.IsCancelable())
        {
            e = Events.Pop();
            e.Cancel(); // Pop until first cancelable
        }
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

    public static bool IsTargetter(GameEvent gameEvent)
    {
        if (gameEvent.GetType().IsSubclassOf(typeof(Targetter<Character>)))
            return true;
        if (gameEvent.GetType().IsSubclassOf(typeof(Targetter<Card>)))
            return true;
        return false;
    }
}
