﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectHealBlackBlood : CardEffect
{
    public override void Play(Character caster)
    {
        int heal = CombatManager.Instance.Player.Characteristics.Attack;
        CombatManager.Instance.Player.Status.Heal(heal);
    }
}
