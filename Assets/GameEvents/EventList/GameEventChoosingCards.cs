using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventChoosingCards : GameEvent
{
    public static string Name = "Choosing Cards";

    public GameEventChoosingCards() : base(null, new List<Targetable>()) { }
 
    public override string GetName()
    {
        return Name;
    }
    public override bool IsFinished()
    {
        return false;
    }
}
