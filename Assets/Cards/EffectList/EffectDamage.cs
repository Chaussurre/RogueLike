using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectDamage : EffectCharacterTarget
{
    [SerializeField]
    int damage;
    protected override void TargetAction(Character caster, Character target)
    {
        EventManager.Push(new GameEventDealDamage(caster, new List<Character>() { target }, damage));
    }
}
