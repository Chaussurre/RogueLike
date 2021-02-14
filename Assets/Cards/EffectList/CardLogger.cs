using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardLogger : CardEffect
{
    public string message = "HEY YOU PLAYED A CARD CONGRATS";

    public override void Play()
    {
        Debug.Log(message);
    }
}
