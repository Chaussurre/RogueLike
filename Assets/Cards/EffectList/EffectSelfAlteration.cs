using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectSelfAlteration : CardEffect
{
    [SerializeField]
    StatusAlteration AlterationPrefab;

    public override void Play(Character caster)
    {
        caster.Status.StatusAlteration.AddAlteration(AlterationPrefab);
    }
}
