using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Element
{
    Ether, Fire, Water, Earth, Air, None
}

public class EffectElement : CardEffect
{
    static Element LastElement = Element.None;

    [SerializeField]
    Element Element;
    public override void Play()
    {
        LastElement = Element;
    }

    public override bool CanPlay()
    {
        return Element != LastElement;
    }
}
