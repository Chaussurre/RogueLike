using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public abstract class EffectCharacterTarget : CardEffect
{
    [SerializeField]
    protected int NbTargets = 1;
    protected CharacterTargetter Targetter;

    protected GameEventManager EventManager;
    public override void OnStack(Character caster)
    {
        EventManager = CombatManager.Instance.EventManager;
        Targetter = new CharacterTargetter(caster, caster.Team, NbTargets);

        EventManager.Push(Targetter);
    }

    public override void Play(Character caster)
    {
        foreach (Character character in Targetter.TargetsCharacter)
            TargetAction(caster, character);
    }

    protected abstract void TargetAction(Character caster, Character target);

}
