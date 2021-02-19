using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectSummon : CardEffect
{
    [SerializeField]
    int NumberSummons = 1;
    [SerializeField]
    Character SummonPrefab;
    public override void Play()
    {
        for (int i = 0; i < NumberSummons; i++)
        {
            Character summon = Instantiate(SummonPrefab);
            CombatManager.Instance.TeamManager.AddCharacter(summon);
        }
    }
}
