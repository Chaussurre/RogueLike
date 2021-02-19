using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectHeal : CardEffect
{
    [SerializeField]
    protected int heal;

    public override void Play()
    {
        CombatManager.Instance.Player.Status.Heal(heal);
    }
}
