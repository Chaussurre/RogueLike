﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardHolder : MonoBehaviour
{
    [SerializeField]
    float HolderSize;
    [SerializeField]
    float HoveredCardSpace;
    [SerializeField]
    float HoveredCardUp;
    [SerializeField]
    float CardPlayHeight;
    readonly List<Card> Cards = new List<Card>();
    Card CardGrabbed = null;

    void Start()
    {
        Draw(10);
    }

    public void Draw(int times = 1)
    {
        for (int i = 0; i < times; i++)
        {
            Card c = CardManager.Deck.TakeFirst();
            if (c == null)
                break;
            Cards.Add(c);
        }
    }

    private void Update()
    {
        int CardIndex = FindHoveredCard();
        if (CardIndex >= 0)
            GroupHoveredCard(CardIndex);
        else
            NoCardHoveredGroup();

        TryPlayCard();
        TryGrabCard(CardIndex);

        if (Cards.Count < 5)
            Draw();
    }
    
    public void AddCard(Card card)
    {
        if (card == null)
            return;

        card.transform.parent = transform.parent;
        Cards.Add(card);
    }

    public void RemoveCard(Card card)
    {
        Cards.Remove(card);
        card.Discard();
    }

    void TryGrabCard(int HoveredCard)
    {
        if (HoveredCard == -1)
            return;

        if (!Input.GetMouseButton(0))
            CardGrabbed = null;
        if (Input.GetMouseButtonDown(0))
            CardGrabbed = Cards[HoveredCard];
    }

    void TryPlayCard()
    {
        float CardHeight = Camera.main.ScreenToWorldPoint(Input.mousePosition).y;

        if (CardGrabbed != null &&
            CardHeight >= CardPlayHeight + transform.position.y &&
            Input.GetMouseButtonUp(0))
        {
            CardGrabbed.Play();
            RemoveCard(CardGrabbed);
        }
    }

    int FindHoveredCard() //return index, -1 if no card found
    {
        int index = -1;
        for (int i = 0; i < Cards.Count; i++)
        {
            if (Cards[i] == CardGrabbed) //priority is given to the grabbed card
                return i;
            if (Cards[i].body.IsHovered())
                index = i;
        }
        return index;
    }

    void NoCardHoveredGroup()
    {
        Vector2 StartPos = transform.position + Vector3.left * (HolderSize / 2f);
        Vector2 EndtPos = transform.position + Vector3.right * (HolderSize / 2f);

        GroupCards(StartPos, EndtPos, Cards);
    }

    void GroupHoveredCard(int cardIndex)
    {
        Vector2 StartPos = transform.position + Vector3.left * (HolderSize / 2f);
        Vector2 EndPos = transform.position + Vector3.right * (HolderSize / 2f);

        Vector2 CardPosition = Vector2.Lerp(StartPos, EndPos, GetFloatCardPosition(cardIndex, Cards.Count));

        Vector2 StartCardSpace = CardPosition + Vector2.left * (HoveredCardSpace / 2f);
        Vector2 EndCardSpace = CardPosition + Vector2.right * (HoveredCardSpace / 2f);

        Debug.DrawLine(StartPos, StartCardSpace, Color.blue);
        Debug.DrawLine(StartCardSpace + Vector2.up, EndCardSpace + Vector2.up, Color.green);
        Debug.DrawLine(EndCardSpace, EndPos, Color.red);

        GroupCards(StartPos, StartCardSpace, Cards.GetRange(0, cardIndex));
        GroupCards(EndCardSpace, EndPos, Cards.GetRange(cardIndex + 1, Cards.Count - cardIndex - 1));

        if (Cards[cardIndex] != CardGrabbed)
            Cards[cardIndex].body.SetPosition(CardPosition + Vector2.up * HoveredCardUp);
        else
            Cards[cardIndex].body.SetPosition(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        Cards[cardIndex].body.SetPriority(Cards.Count);
    }

    void GroupCards(Vector2 StartPos, Vector2 EndPos, List<Card> Cards)
    {
        if (StartPos.x > EndPos.x) //switching Start and end if nescessary
        {
            Vector2 tmp = StartPos;
            StartPos = EndPos;
            EndPos = tmp;
        }

        for(int i = 0; i < Cards.Count; i++)
        {
            Vector2 position = Vector2.Lerp(StartPos, EndPos, GetFloatCardPosition(i, Cards.Count));
            Cards[i].body.SetPosition(position);
            Cards[i].body.SetPriority(i + 1);
        }
    }

    float GetFloatCardPosition(int CardIndex, int IndexMax)
    {
        return ((float) CardIndex + 1) / (IndexMax + 1);
    }

    public void PlayCard(Card card)
    {
        card.Play();
        Cards.Remove(card);
    }

    public float GetPlayHeight()
    {
        return transform.position.y + CardPlayHeight;
    }
}
