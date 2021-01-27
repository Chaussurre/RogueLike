using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardLogger : Card
{
    public override void Play()
    {
        Debug.Log("HEY YOU PLAYED A CARD CONGRATS");
    }
}
