using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardHolder : MonoBehaviour
{
    public List<Card> Cards { get; private set; } = new List<Card>();

    private Deck Deck;
    private PlayerCharacter Player;

    void Start()
    {
        Player = CombatManager.Instance.Player;
        Deck = CombatManager.Instance.CardManager.Deck;
        Draw(10);
    }

    public void Draw(int times = 1)
    {
        for (int i = 0; i < times; i++)
        {
            Card c = Deck.TakeFirst();
            if (c == null)
                break;
            c.transform.parent = transform;
            Cards.Add(c);
        }
    }
    
    public void AddCard(Card card)
    {
        if (card == null)
            return;

        card.transform.parent = transform;
        Cards.Add(card);
    }

    public void RemoveCard(Card card)
    {
        Cards.Remove(card);
    }

    public void PlayCard(Card card)
    {
        Player.TryPlayCard(card);
    }
}
