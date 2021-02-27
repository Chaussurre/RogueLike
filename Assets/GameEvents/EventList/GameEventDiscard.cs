using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventDiscard : GameEvent
{
    Card Card;

    public GameEventDiscard(Card Card) : base(null, new List<Targetable>() { })
    {
        this.Card = Card;
    }

    public override void Trigger()
    {
        CombatManager.Instance.CardManager.CardHolder.RemoveCard(Card);
        if (Card.body != null)
            Card.body.Discard();
    }
}
