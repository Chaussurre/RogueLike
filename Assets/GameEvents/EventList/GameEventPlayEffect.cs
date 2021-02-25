using System;
using System.Collections.Generic;
using UnityEngine;
class GameEventPlayEffect : GameEvent
{
    CardEffect Effect;
    public GameEventPlayEffect(Character Source, CardEffect Effect) : base(Source, new List<Targetable>() { }) 
    {
        this.Effect = Effect;
    }

    public override string GetName()
    {
        return "Play effect";
    }

    public override void Trigger() 
    {
        if (Effect.CanPlay())
            Effect.Play();
        CombatManager.Instance.EventManager.Push(new GameEventWait(0.5f));
    }
}
