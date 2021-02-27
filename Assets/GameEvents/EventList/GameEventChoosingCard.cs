using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventChoosingCard : GameEvent
{
    Card Card;
    public GameEventChoosingCard(Card Card) : base(CombatManager.Instance.Player, new List<Targetable>() { Card })
    {
        this.Card = Card;
    }

    public override void Trigger()
    {
        Source.Status.PayMana(Card.ManaCost);
        CombatManager.Instance.EventManager.Push(new GameEventPlayCard(Source, Card));
        CombatManager.Instance.CardManager.CardHolder.RemoveCard(Card);
    }
}
