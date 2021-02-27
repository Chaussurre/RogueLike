using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventPlayCardShuffle : GameEvent
{
    Card Card;
    public GameEventPlayCardShuffle(Card Card): base(null, new List<Targetable>() { })
    {
        this.Card = Card;
    }

    public override void Trigger()
    {
        CombatManager.Instance.CardManager.CardHolder.RemoveCard(Card);
        CombatManager.Instance.CardManager.Deck.ShuffleCard(Card);
        if (Card.body != null)
            Card.body.Reshuffle();
    }
}
