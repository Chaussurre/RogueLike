﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectLogger : CardEffect
{
    public string message = "HEY YOU PLAYED A CARD CONGRATS";

    public override void Play(Character caster)
    {
        Debug.Log(message);
    }
}
