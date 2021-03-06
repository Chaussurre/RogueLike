﻿using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public abstract class Targetter<TargetType> : GameEvent
{
    protected readonly int NbTargets;

    protected readonly HashSet<TargetType> Candidates = new HashSet<TargetType>();

    public readonly List<TargetType> TargetsCharacter = new List<TargetType>();

    GameEventManager EventManager;
    public Targetter(Character Source, int NbTargets = 1) : base(Source, new List<Targetable>() { }) 
    {
        this.NbTargets = NbTargets;
        EventManager = CombatManager.Instance.EventManager;
    }

    public void RemoveWhere(Predicate<TargetType> Predicate)
    {
        Candidates.RemoveWhere(Predicate);
    }

    public override bool IsFinished()
    {
        return TargetsCharacter.Count >= NbTargets || TargetsCharacter.Count >= Candidates.Count;
    }

    public override void OnWait()
    {
        Debug.DrawLine(Vector3.zero, Camera.main.ScreenToWorldPoint(Input.mousePosition), Color.green);

        TryTarget(TargetsCharacter);
    }

    private void TryTarget(List<TargetType> Targets)
    {
        if (Source != CombatManager.Instance.Player) //Not the player
            AutoTarget(Targets);
        else if (Input.GetMouseButtonUp(0)) //The player is clicking
        {
            Vector2 MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (TargetAtPosition(MousePos, Targets))
                EventManager.TargetterManager.CreateArrow((Targetable) Targets[Targets.Count -1]);
        }
        else if (Input.GetMouseButtonDown(1))
            EventManager.Cancel();
    }

    protected abstract bool TargetAtPosition(Vector2 mousePosisition, List<TargetType> targets);
    public abstract void AutoTarget(List<TargetType> targets);
}
