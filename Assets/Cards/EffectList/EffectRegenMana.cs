using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectRegenMana : CardEffect
{
    [SerializeField]
    int ManaAmount;

    public override void Play()
    {
        CombatManager.Instance.Player.Status.ManaRegen(ManaAmount);
    }
}
