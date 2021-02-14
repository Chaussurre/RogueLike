using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    public List<Card> Cards;
    private SpriteRenderer renderer;

    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        CheckNonEmpty();
        for(int i = 0; i < 25; i++)
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
        return c;
    }

    private void CheckNonEmpty()
    {
        renderer.enabled = Cards.Count > 0;
    }
}
