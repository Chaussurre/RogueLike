using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TargetterManager))]
public class GameEventManager : MonoBehaviour
{
    [HideInInspector]
    public TargetterManager TargetterManager;

    public readonly Stack<GameEvent> Events = new Stack<GameEvent>();
    public readonly HashSet<GameEventWatcher> Watchers = new HashSet<GameEventWatcher>();

    private void Start()
    {
        TargetterManager = GetComponent<TargetterManager>();
    }

    public bool IsEmpty()
    {
        while(Events.Count > 0 && Events.Peek().IsFinished())
            PlayEvent();

        return Events.Count == 0;
    }

    public void Push(GameEvent gameEvent)
    {
        if (IsTargetter(this))
            TargetterManager.StopTargetting();

        Events.Push(gameEvent);
        gameEvent.OnStack();

        if (IsTargetter(gameEvent))
            TargetterManager.CreateArrow(null);
    }

    public void Wait(float Timer)
    {
        Events.Peek().Wait(Timer);

    }

    private void PlayEvent()
    {
        GameEvent gameEvent = Pop();

        foreach (GameEventWatcher watcher in Watchers)
            if (watcher.Check(gameEvent) && !watcher.IsAllowed(gameEvent))
                return;

        gameEvent.Trigger();

        foreach (GameEventWatcher watcher in Watchers)
            if (watcher.Check(gameEvent))
                watcher.OnTrigger(gameEvent);
    }

    public GameEvent Pop()
    {
        if (IsTargetter(this))
            TargetterManager.StopTargetting();

        GameEvent gameEvent = Events.Pop();

        if (IsTargetter(this))
            TargetterManager.CreateArrow(null);

        return gameEvent;
    }

    public void Pop(System.Type type)
    {
        if (Events.Peek().GetType() == type)
            Pop();
    }

    public void Cancel()
    {
        GameEvent e = Pop();
        while (!e.IsCancelable())
        {
            e = Pop();
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

    public static bool IsTargetter(GameEventManager manager)
    {
        if (manager.Events.Count == 0)
            return false;
        return IsTargetter(manager.Events.Peek());

    }
}
