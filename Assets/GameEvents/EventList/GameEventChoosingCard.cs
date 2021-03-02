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

    public override void OnStack()
    {
        CombatManager.Instance.EventManager.Push(new GameEventPlayCard(Source, Card));
        Card.body.SetPosition(Vector2.zero);
        Card.body.Lock();
        CombatManager.Instance.EventManager.Push(new GameEventWait(0.5f));
    }

    public override void Trigger()
    {
        Source.Status.PayMana(Card.ManaCost);
        CombatManager.Instance.CardManager.CardHolder.RemoveCard(Card);
    }

    public override bool IsCancelable() { return true; }

    public override void Cancel()
    {
        Card.body.Lock(false);
    }
}
