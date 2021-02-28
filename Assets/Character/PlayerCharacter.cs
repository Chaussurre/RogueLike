using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PlayerCharacter : Character
{
    GameEventManager EventManager;

    protected override void Start()
    {
        base.Start();
        EventManager = CombatManager.Instance.EventManager;
        GameEventWatcher watcher = new GameEventWatcher(typeof(GameEventStartTurn), this, default);
        watcher.SetOnTrigger(ActionStartTurnDraw);
        EventManager.AddWatcher(watcher);
    }
    private void ActionStartTurnDraw(GameEvent _)
    {
        EventManager.Push(new GameEventDraw(this, 1));
    }

    protected override void PlayTurn()
    {
        EventManager.Push(new GameEventEndTurn(this));
        Attack();
        EventManager.Push(new GameEventPlayerTurn());
        EventManager.Push(new GameEventStartTurn(this));
    }

    public void EndTurn()
    {
        EventManager.Pop(typeof(GameEventPlayerTurn));
    }

    protected override void PlayEffect(){}

    public bool TryPlayCard(Card Card)
    {
        if (EventManager.GetActiveEvent() != typeof(GameEventPlayerTurn))
            return false;
        if (Status.Mana < Card.ManaCost)
            return false;
        if (!Card.CanPlay())
            return false;

        EventManager.Push(new GameEventChoosingCard(Card));
        return true;
    }
}
