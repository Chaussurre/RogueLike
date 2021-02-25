using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : Character
{
    private CardHolder CardHolder;

    protected override void Start()
    {
        CardHolder = CombatManager.Instance.CardManager.CardHolder;
        base.Start();
    }

    protected override void PlayTurn()
    {
        CombatManager.Instance.EventManager.Push(new GameEventEndTurn(this));
        Attack();
        CombatManager.Instance.EventManager.Push(new GameEventChoosingCards());
        CombatManager.Instance.EventManager.Push(new GameEventStartTurnPlayer());
    }


    protected override void PlayEffect(){}

    public bool TryPlayCard(Card card)
    {
        if (CombatManager.Instance.EventManager.GetActiveEvent() != GameEventChoosingCards.Name)
            return false;
        if (Status.Mana < card.ManaCost)
            return false;
        if (!card.CanPlay())
            return false;
        if (!Status.StatusAlteration.AllowPlay(card))
            return false;

        PlayCard(card);
        return true;
    }

    private void PlayCard(Card card)
    {
        Status.PayMana(card.ManaCost);
        CombatManager.Instance.EventManager.Push(new GameEventPlayCard(this, card));
        CardHolder.RemoveCard(card);
    }
}
