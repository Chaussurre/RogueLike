using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventDraw : GameEvent
{
    int NbCards;

    public GameEventDraw(Character source, int NbCards) : base(source, new List<Targetable>() { CombatManager.Instance.Player })
    {
        this.NbCards = NbCards;
    }

    public override string GetName()
    {
        return "Draw Cards";
    }

    public override void Trigger()
    {
        CombatManager.Instance.CardManager.CardHolder.Draw(NbCards);
    }
}
