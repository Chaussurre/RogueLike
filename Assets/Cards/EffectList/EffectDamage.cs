using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectDamage : CardEffect
{
    [SerializeField]
    int damage;
    public override void Play(Character caster)
    {
        Character Target = CombatManager.Instance.BadGuy;
        CombatManager.Instance.EventManager.Push(new GameEventDealDamage(caster, new List<Character>() { Target }, damage));
    }
}
