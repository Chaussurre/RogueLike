using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventDealDamage : GameEvent
{
    HashSet<Character> TargetsCharacter = new HashSet<Character>();
    int Amount;

    public GameEventDealDamage(Character Source, IEnumerable<Character> Targets, int Amount) : base(Source, Targets)
    {
        TargetsCharacter.UnionWith(Targets);
        this.Amount = Amount;
    }

    public override void Trigger()
    {
        foreach (Character Target in TargetsCharacter)
            Target.Status.DealDammage(Amount);
    }
}
