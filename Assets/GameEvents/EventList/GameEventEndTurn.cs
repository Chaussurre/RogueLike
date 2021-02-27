using System;
using System.Collections.Generic;
using UnityEngine;
class GameEventEndTurn : GameEvent
{
    public GameEventEndTurn(Character Source) : base(Source, new List<Targetable>()) { }
}
