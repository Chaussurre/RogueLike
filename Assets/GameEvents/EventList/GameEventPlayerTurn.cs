using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventPlayerTurn : GameEvent
{
    public GameEventPlayerTurn() : base(null, new List<Targetable>()) { }
 
    public override bool IsFinished()
    {
        return false;
    }
}
