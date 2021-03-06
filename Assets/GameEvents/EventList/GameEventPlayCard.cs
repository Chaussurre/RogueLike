﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventPlayCard : GameEvent
{
    Card Card;
    public GameEventPlayCard(Character Source, Card Card) : base(Source, new List<Targetable>() { Card })
    {
        this.Card = Card;
    }

    public override void OnStack()
    {
        Card.PushEffects(Source);
    }

    public override void Trigger()
    {
        Card.Play(Source);
    }
}
