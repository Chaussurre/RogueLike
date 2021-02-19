using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectSelfDamage : CardEffect
{
    [SerializeField]
    int Damage;

    public override void Play()
    {
        CombatManager.Instance.Player.Status.DealDammage(Damage);
    }
}