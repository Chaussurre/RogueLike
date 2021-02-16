using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : Character
{
    public static PlayerCharacter Instance;

    public bool CanPlay { get; private set; } = false; //When the player decide he played all his card

    private void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);
        else
            Instance = this;
    }

    public override void StartTurn()
    {
        CanPlay = true;
        CardHolder.Instance.Draw();
        base.StartTurn();
    }

    protected override bool PlayEffect()
    {
        return !CanPlay;
    }

    public void StopPlayingCards()
    {
        CanPlay = false;
    }

    public bool TryPlayCard(Card card)
    {
        if (!CanPlay)
            return false;
        if (Status.Mana < card.ManaCost)
            return false;
        PlayCard(card);
        return true;
    }

    private void PlayCard(Card card)
    {
        Status.PayMana(card.ManaCost);
        card.Play();
        CardHolder.Instance.RemoveCard(card);
    }
}
