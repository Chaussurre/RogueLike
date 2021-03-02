using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectHeal : CardEffect
{
    [SerializeField]
    protected int heal;
    [SerializeField]
    protected int NbTargets = 1;
    CharacterTargetter Targetter;

    GameEventManager EventManager;
    public override void OnStack(Character caster)
    {
        EventManager = CombatManager.Instance.EventManager;
        Targetter = new CharacterTargetter(caster, caster.Team, NbTargets);
        Targetter.SetPredicate(IsHurt); //Can't heal a full life character

        EventManager.Push(Targetter);
    }

    public override bool CanPlay()
    {
        return true;//!Targetter.Cancelled;
    }

    public override void Play(Character caster)
    {
        EventManager.Push(new GameEventHeal(caster, Targetter.TargetList, heal));
    }

    private bool IsHurt(Character character)
    {
        return character.Status.Hp < character.Status.characteristics.Hp;
    }
}
