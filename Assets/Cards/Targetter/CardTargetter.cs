using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardTargetter : Targetter<Card>
{
    CardHolder CardHolder;
    public CardTargetter(Character Source, int NbTargets = 1) : base(Source, NbTargets)
    {
        CardHolder = CombatManager.Instance.CardManager.CardHolder;
        foreach (Card card in CardHolder.Cards)
            Candidates.Add(card);
    }

    protected override bool TargetAtPosition(Vector2 mousePosisition, List<Card> targets)
    {
        if (CardHolder.Positionner.CardHovered == null)
            return false;

        Card card = CardHolder.Positionner.CardHovered;

        if (targets.Contains(card))
            return false;

        targets.Add(card);
        return true;
    }

    public override void AutoTarget(List<Card> targets)
    {
        List<Card> Tmp = new List<Card>();
        foreach (Card  card in Candidates)
            Tmp.Insert(Random.Range(0, Tmp.Count + 1), card);

        int MaxNbTargets = Mathf.Min(NbTargets, Tmp.Count);
        targets.AddRange(Tmp.GetRange(0, MaxNbTargets));
    }
}
