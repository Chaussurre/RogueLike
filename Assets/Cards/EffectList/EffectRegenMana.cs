using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectRegenMana : CardEffect
{
    [SerializeField]
    int ManaAmount;

    public override void Play(Character caster)
    {
        caster.Status.IncreaseMana(ManaAmount);
    }
}
