using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectHeal : CardEffect
{
    [SerializeField]
    protected int heal;

    public override void Play()
    {
        PlayerCharacter Player = CombatManager.Instance.Player;
        CombatManager.Instance.EventManager.Push(new GameEventHeal(Player, new List<Character>() { Player }, heal));
    }
}
