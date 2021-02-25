using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventStartTurn : GameEvent
{

    public override string GetName()
    {
        return "Start Turn";
    }

    public GameEventStartTurn(Character Source) : base(Source, new List<Targetable>()) { }
}
