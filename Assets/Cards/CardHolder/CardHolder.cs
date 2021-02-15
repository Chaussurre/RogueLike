using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardHolder : MonoBehaviour
{
    public static CardHolder Instance = null;
    public List<Card> Cards { get; private set; } = new List<Card>();

    void Start()
    {
        if (Instance != null)
            Destroy(gameObject);
        else
            Instance = this;

        Draw(10);
    }

    private void Update()
    {
        if (Cards.Count < 5)
            Draw();
    }

    public void Draw(int times = 1)
    {
        for (int i = 0; i < times; i++)
        {
            Card c = CardManager.Deck.TakeFirst();
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
        card.Discard();
    }

    public void PlayCard(Card card)
    {
        if (!Hero.Instance.CanPlay)
            return;

        card.Play();
        RemoveCard (card);
    }
}
