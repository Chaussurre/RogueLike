using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    [HideInInspector]
    public Deck Deck = null;
    [HideInInspector]
    public CardHolder CardHolder = null;

    public CardBody BodyPrefab;

    [SerializeField]
    public List<Card> ListCards = new List<Card> { };

    // Start is called before the first frame update
    void Awake()
    {
        Deck = FindObjectOfType<Deck>();
        CardHolder = FindObjectOfType<CardHolder>();
    }
}
