using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectSelfAlteration : CardEffect
{
    [SerializeField]
    StatusAlteration AlterationPrefab;

    public override void Play()
    {
        CombatManager.Instance.Player.Status.StatusAlteration.AddAlteration(AlterationPrefab);
    }
}
