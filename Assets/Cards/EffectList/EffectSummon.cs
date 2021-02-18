using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectSummon : CardEffect
{
    public Character SummonPrefab;
    public override void Play()
    {
        Character summon = Instantiate(SummonPrefab);
        CombatManager.Instance.TeamManager.AddCharacter(summon);
    }
}
