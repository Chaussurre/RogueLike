using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventHeal : GameEvent
{
    int Amount;
    HashSet<Character> Characters = new HashSet<Character>();

    public GameEventHeal(Character Character, IEnumerable<Character> Targets, int Heal) : base(Character, Targets)
    {
        Amount = Heal;
        Characters.UnionWith(Targets);
    }

    public override void Trigger()
    {
        foreach (Character character in Characters)
            character.Status.Heal(Amount);
    }
}
