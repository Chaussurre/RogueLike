using System.Collections;
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
    readonly List<Card> Cards = new List<Card>();

    public static CardHolder Instance = null;
    
    void Start()
    {
        if (Instance != null)
            Destroy(gameObject);

        Cards.AddRange(FindObjectsOfType<Card>());

        foreach (Card c in Cards)
            c.transform.parent = transform;

        Instance = this;
    }

    private void Update()
    {
        int CardIndex = FindHoveredCard();
        if (CardIndex >= 0)
            GroupHoveredCard(CardIndex);
        else
            NoCardHoveredGroup();
    }

    int FindHoveredCard() //return index, -1 if no card found
    {
        for (int i = 0; i < Cards.Count; i++)
            if (Cards[i].IsHovered() || Cards[i].IsGrabbed())
                return i;
        return -1; //No card found
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

        float floatCardPosition = ((float) cardIndex) / (Cards.Count - 1);
        Vector2 CardPosition = Vector2.Lerp(StartPos, EndPos, floatCardPosition);

        Vector2 StartCardSpace = CardPosition + Vector2.left * (HoveredCardSpace / 2f);
        Vector2 EndCardSpace = CardPosition + Vector2.right * (HoveredCardSpace / 2f);

        Debug.DrawLine(StartPos, StartCardSpace, Color.blue);
        Debug.DrawLine(StartCardSpace + Vector2.up, EndCardSpace + Vector2.up, Color.green);
        Debug.DrawLine(EndCardSpace, EndPos, Color.red);

        GroupCards(StartPos, StartCardSpace, Cards.GetRange(0, cardIndex));
        GroupCards(EndCardSpace, EndPos, Cards.GetRange(cardIndex + 1, Cards.Count - cardIndex - 1));

        if (!Cards[cardIndex].IsGrabbed())
            Cards[cardIndex].SetPosition(CardPosition + Vector2.up * HoveredCardUp);
        Cards[cardIndex].SetPriority(0);
    }

    void GroupCards(Vector2 StartPos, Vector2 EndPos, List<Card> Cards)
    {
        if (StartPos.x > EndPos.x) //switching Start and end if nescessary
        {
            Vector2 tmp = StartPos;
            StartPos = EndPos;
            EndPos = tmp;
        }

        if(Cards.Count == 1)
        {
            Vector2 position = Vector2.Lerp(StartPos, EndPos, 0.5f);
            Cards[0].SetPosition(position);
            Cards[0].SetPriority(1);
            return;
        }

        for(int i = 0; i < Cards.Count; i++)
        {
            float floatPosition = ((float) i) / (Cards.Count - 1);
            Vector2 position = Vector2.Lerp(StartPos, EndPos, floatPosition);
            Cards[i].SetPosition(position);
            Cards[i].SetPriority(i + 1);
        }
    }
}
