using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AlterationElementOverload : StatusAlteration
{
    [SerializeField]
    public Element Element;

    public override void AddStacks(StatusAlteration otherPrefab)
    {
        if (otherPrefab.TryGetComponent(out AlterationElementOverload otherOverload))
            Element = otherOverload.Element;
    }

    public override bool AllowPlay(Card card)
    {
        if (card.TryGetComponent(out IsOfElement other))
            return other.Element != Element;

        return true;
    }
}
