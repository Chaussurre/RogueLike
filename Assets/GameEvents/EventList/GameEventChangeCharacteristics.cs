using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventChangeCharacteristics : GameEvent
{
    Characteristics Characteristics;

    public GameEventChangeCharacteristics(Character Source, IEnumerable<Character> Targets, Characteristics Characteristics) : base(Source, Targets)
    {
        this.Characteristics = Characteristics;
    }

    public override void Trigger()
    {
        foreach(Character character in Targets)
        {
            character.Characteristics.AddCharacteristics(Characteristics);
        }
    }
}
