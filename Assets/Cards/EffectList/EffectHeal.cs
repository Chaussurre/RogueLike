using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectHeal : EffectCharacterTarget
{
    [SerializeField]
    int Heal;

    protected override void TargetAction(Character caster, Character target)
    {
        EventManager.Push(new GameEventHeal(caster, new List<Character>() { target }, Heal));
    }
}
