using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventStartTurnPlayer : GameEventStartTurn
{
    public GameEventStartTurnPlayer() : base(CombatManager.Instance.Player) { }
 
    public override void Trigger()
    {
        CombatManager.Instance.EventManager.Push(new GameEventDraw(Source, 1));
    }
}