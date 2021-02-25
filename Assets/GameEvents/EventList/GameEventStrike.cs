using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventStrike : GameEvent
{
    float StrikeTime = 0.5f;
    public Character Target { get; private set; }

    public GameEventStrike(Character Source, Character Target) : base(Source, new List<Targetable>() { Target })
    {
        this.Target = Target;
    }

    public override string GetName()
    {
        return "Strike";
    }

    public override void Trigger()
    {
        Target.Status.DealDammage(Source.Characteristics.Attack);
        Source.body.Strike();
        CombatManager.Instance.EventManager.Push(new GameEventWait(0.5f));
    }
}
