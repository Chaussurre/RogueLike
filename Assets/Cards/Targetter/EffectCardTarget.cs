using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EffectCardTarget : CardEffect
{
    [SerializeField]
    protected int NbTargets = 1;
    protected CardTargetter Targetter;

    protected GameEventManager EventManager;
    public override void OnStack(Character caster)
    {
        EventManager = CombatManager.Instance.EventManager;
        Targetter = new CardTargetter(caster, NbTargets);

        EventManager.Push(Targetter);
    }

    public override void Play(Character caster)
    {
        foreach (Card card in Targetter.TargetsCharacter)
            TargetAction(caster, card);
    }

    protected abstract void TargetAction(Character caster, Card target);
}
