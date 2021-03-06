﻿using System;
using System.Collections.Generic;
using UnityEngine;
class GameEventPlayEffect : GameEvent
{
    CardEffect Effect;
    public GameEventPlayEffect(Character Source, CardEffect Effect) : base(Source, new List<Targetable>() { }) 
    {
        this.Effect = Effect;
    }

    public override void Trigger() 
    {
        if (Effect.CanPlay())
            Effect.Play(Source);
    }
}
