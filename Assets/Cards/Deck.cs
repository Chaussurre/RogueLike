using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    public List<Card> Cards;
    private SpriteRenderer renderer;

    private CardManager CardManager;

    // Start is called before the fizrst frame update
    void Start()
    {
        CardManager = CombatManager.Instance.CardManager;
        renderer = GetComponent<SpriteRenderer>();
        List<Card> ListCards = CardManager.ListCards;
        CheckNonEmpty();
        for(int i = 0; i < 20; i++)
        {
            Card c = Instantiate(ListCards[i % ListCards.Count]);
            Add(c);
        }
    }

    public void ShuffleCard(Card card)
    {
        AddAt(card, Random.Range(0, Cards.Count + 1));
    }

    public void Add(Card c)
    {
        AddAt(c, 0);
    }

    public void AddAt(Card c, int index)
    {
        if (index > Cards.Count)
            Cards.Add(c);
        else
            Cards.Insert(index, c);
        c.transform.parent = transform;
        CheckNonEmpty();
    }

    public Card TakeFirst()
    {
        if (Cards.Count == 0)
            return null;

        return TakeAt(0);
    }

    public Card TakeAt(int index)
    {
        if (Cards.Count == 0)
            return null;

        if (index >= Cards.Count)
            index = Cards.Count - 1;

        Card c = Cards[index];
        Cards.RemoveAt(index);
        CheckNonEmpty();
        c.CreateBody(transform.position);
        c.transform.parent = CardManager.transform;
        return c;
    }

    private void CheckNonEmpty()
    {
        renderer.enabled = Cards.Count > 0;
    }
}
