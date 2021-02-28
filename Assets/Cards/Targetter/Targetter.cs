using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public abstract class Targetter<TargetType> : GameEvent
{
    private Predicate<TargetType> Predicate = null;
    private bool FoundTarget = false;

    public readonly List<TargetType> TargetList = new List<TargetType>();
    public bool Cancelled { get; protected set; } = false;
    public Targetter(Character Source) : base(Source, new List<Targetable>() { }) { }

    protected bool Check(TargetType target)
    {
        return Predicate == null || Predicate(target);
    }

    public void SetPredicate(Predicate<TargetType> Predicate)
    {
        this.Predicate = Predicate;
    }

    public override bool IsFinished()
    {
        return FoundTarget || Cancelled;
    }

    public override void OnWait()
    {
        Debug.DrawLine(Vector3.zero, Camera.main.ScreenToWorldPoint(Input.mousePosition));
        if (TryTarget(TargetList))
        {
            if (Targets.Count == 0)
                Cancelled = true;
            else
                FoundTarget = true;
        }
    }

    private bool TryTarget(List<TargetType> Targets)
    {
        if (Source != CombatManager.Instance.Player)
        {
            AutoTarget(Targets);
            return true;
        }
        else if (Input.GetMouseButtonDown(0))
        {
            Vector2 MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Debug.DrawLine(Vector2.zero, MousePos, Color.red);
            return TargetAtPosition(MousePos, Targets);
        }

        return false;
    }

    protected abstract bool TargetAtPosition(Vector2 mousePosisition, List<TargetType> targets);
    public abstract void AutoTarget(List<TargetType> targets);
}
