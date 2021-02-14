using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    public List<Card> Cards;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < 15; i++)
        {
            Card c = Instantiate(CardManager.Instance.ListCards[0]);
            Add(c);
        }
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
    }

    public Card TakeFirst()
    {
        if (Cards.Count == 0)
            return null;

        Card c = Cards[0];
        Cards.RemoveAt(0);
        return c;
    }
}
