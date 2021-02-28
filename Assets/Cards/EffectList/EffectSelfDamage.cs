using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectSelfDamage : CardEffect
{
    [SerializeField]
    int Damage;

    public override void Play(Character caster)
    {
        caster.Status.DealDammage(Damage);
    }
}