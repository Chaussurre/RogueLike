using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameEvent
{
    protected float Timer = 0;
    public Character Source { get; private set; }
    public readonly HashSet<Targetable> Targets = new HashSet<Targetable>();
    public GameEvent(Character Source, IEnumerable<Targetable> Targets)
    {
        this.Source = Source;
        this.Targets.UnionWith(Targets);
    }

    public void Wait(float Timer)
    {
        this.Timer += Timer;

        OnWait();
    }

    public virtual void OnWait() { }

    public virtual void OnStack() { }
    public virtual void Trigger() { }

    public virtual bool IsFinished() { return true; }
    public virtual bool IsCancelable() { return false; }
    public virtual void Cancel() {  }
}
