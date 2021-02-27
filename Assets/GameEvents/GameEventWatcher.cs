using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class GameEventWatcher
{
    public Type WatchedType { private set; get; }

    Character Source;
    readonly Targetable Target;

    private Predicate<GameEvent> Allow = null;
    private Action<GameEvent> Trigger = null;

    public GameEventWatcher(Type Type, Character Source, Targetable Target)
    {
        WatchedType = Type;
        this.Source = Source;
        this.Target = Target;
    }

    public bool Check(GameEvent gameEvent)
    {
        if (gameEvent.GetType() != WatchedType)
            return false;

        if (Source != null && Source != gameEvent.Source)
            return false;

        if (!gameEvent.Targets.Contains(Target))
            return false;

        return true;
    }

    public void SetAllow(Predicate<GameEvent> predicate) 
    {
        Allow = predicate;
    }

    public void SetOnTrigger(Action<GameEvent> action) 
    {
        Trigger = action;
    }

    public bool IsAllowed(GameEvent gameEvent)
    {
        return Allow == null || Allow(gameEvent);
    }

    public void OnTrigger(GameEvent gameEvent)
    {
        Trigger?.Invoke(gameEvent);
    }
}
