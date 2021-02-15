using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Set the cards in hand to their correct position
public class CardHolderPositionner : MonoBehaviour
{

    [SerializeField]
    float HolderSize;
    [SerializeField]
    float HoveredCardSpace;
    [SerializeField]
    float HoveredCardUp;
    [SerializeField]
    float CardPlayHeight;
    Card CardGrabbed = null;

    CardHolder holder;
    List<Card> Cards;

    private void Start()
    {
        holder = GetComponent<CardHolder>();
        Cards = holder.Cards;
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
            holder.PlayCard(CardGrabbed);
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
        GroupCards(EndCardSpace, EndPos, Cards.GetRange(cardIndex + 1, Cards.Count - cardIndex - 1), true);

        if (Cards[cardIndex] != CardGrabbed) //Hovered card isn't grabbed so it doesn't follow mouse
            Cards[cardIndex].body.SetPosition(CardPosition + Vector2.up * HoveredCardUp);
        else // Hovered is grabbed so it follow mouse
            Cards[cardIndex].body.SetPosition(Camera.main.ScreenToWorldPoint(Input.mousePosition));

        Cards[cardIndex].body.SetPriority(Cards.Count); //Highest priority for the hovered/grabbed card
    }

    void GroupCards(Vector2 StartPos, Vector2 EndPos, List<Card> Cards, bool reversePriority = false)
    {
        if (StartPos.x > EndPos.x) //switching Start and end if nescessary
        {
            Vector2 tmp = StartPos;
            StartPos = EndPos;
            EndPos = tmp;
        }

        for (int i = 0; i < Cards.Count; i++)
        {
            Vector2 position = Vector2.Lerp(StartPos, EndPos, GetFloatCardPosition(i, Cards.Count));
            Cards[i].body.SetPosition(position);
            if (reversePriority)
                Cards[i].body.SetPriority(Cards.Count - i - 1);
            else
                Cards[i].body.SetPriority(i + 1);
        }
    }

    float GetFloatCardPosition(int CardIndex, int IndexMax)
    {
        return ((float)CardIndex + 1) / (IndexMax + 1);
    }
    public float GetPlayHeight()
    {
        return transform.position.y + CardPlayHeight;
    }
}
