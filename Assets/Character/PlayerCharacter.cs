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
        CombatManager.Instance.EventManager.Push(new GameEventPlayerTurn());
        CombatManager.Instance.EventManager.Push(new GameEventStartTurnPlayer());
    }

    public void EndTurn()
    {
        CombatManager.Instance.EventManager.Pop(typeof(GameEventPlayerTurn));
    }

    protected override void PlayEffect(){}

    public bool TryPlayCard(Card Card)
    {
        if (CombatManager.Instance.EventManager.GetActiveEvent() != typeof(GameEventPlayerTurn))
            return false;
        if (Status.Mana < Card.ManaCost)
            return false;
        if (!Card.CanPlay())
            return false;

        CombatManager.Instance.EventManager.Push(new GameEventChoosingCard(Card));
        return true;
    }
}
