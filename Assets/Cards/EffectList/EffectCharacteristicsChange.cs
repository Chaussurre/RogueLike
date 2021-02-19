using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Characteristics))]
public class EffectCharacteristicsChange : CardEffect
{
    Characteristics Characteristics;

    private void Start()
    {
        Characteristics = GetComponent<Characteristics>();
    }

    public override void Play()
    {
        CombatManager.Instance.Player.Characteristics.AddCharacteristics(Characteristics);
    }
}
