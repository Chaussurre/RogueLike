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

    public override void Play(Character caster)
    {
        Character Target = caster; //TODO allow different targets
        CombatManager.Instance.EventManager.Push(new GameEventChangeCharacteristics(caster, new List<Character>() { Target }, Characteristics));
    }
}
