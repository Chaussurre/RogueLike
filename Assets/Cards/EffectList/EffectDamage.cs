using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectDamage : CardEffect
{
    [SerializeField]
    int damage;
    public override void Play()
    {
        CombatManager.Instance.BadGuy.Status.DealDammage(damage);
        Debug.Log("Dealt " + damage + " damage(s) to enemy");
    }
}
