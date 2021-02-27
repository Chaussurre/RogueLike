using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventWait : GameEvent
{
    float TimeToWait = 0f;

    public GameEventWait(float Time): base(null, new List<Targetable> { })
    {
        TimeToWait = Time;
    }

    public override bool IsFinished()
    {
        return Timer >= TimeToWait;
    }
}
