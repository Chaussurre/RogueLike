using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public abstract class Targetter<TargetType> : GameEvent
{
    private Predicate<TargetType> Predicate = null;
    protected readonly int NbTargets;

    public readonly List<TargetType> TargetList = new List<TargetType>();
    public bool Finished { get; protected set; } = false;
    
    public Targetter(Character Source, int NbTargets = 1) : base(Source, new List<Targetable>() { }) 
    {
        this.NbTargets = NbTargets;
    }

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
        return TargetList.Count == NbTargets || Finished;
    }

    public override void OnWait()
    {
        Debug.DrawLine(Vector3.zero, Camera.main.ScreenToWorldPoint(Input.mousePosition), Color.green);
        Finished = TryTarget(TargetList);
    }

    private bool TryTarget(List<TargetType> Targets)
    {
        if (Source != CombatManager.Instance.Player) //Not the player
        {
            AutoTarget(Targets);
            return true;
        }
        else if (Input.GetMouseButtonDown(0)) //The player is clicking
        {
            Vector2 MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Debug.DrawLine(Vector2.zero, MousePos, Color.red, Time.deltaTime);
            return TargetAtPosition(MousePos, Targets);
        }
        else if (Input.GetMouseButtonDown(1))
            CombatManager.Instance.EventManager.Cancel();
        return false;
    }

    protected abstract bool TargetAtPosition(Vector2 mousePosisition, List<TargetType> targets);
    public abstract void AutoTarget(List<TargetType> targets);
}
