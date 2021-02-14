using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public static CardManager Instance = null;

    public static Deck Deck = null;
    public static CardHolder CardHolder = null;

    public CardBody BodyPrefab;

    [SerializeField]
    public List<Card> ListCards = new List<Card> { };

    // Start is called before the first frame update
    void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);
        else
            Instance = this;

        Deck = FindObjectOfType<Deck>();
        CardHolder = FindObjectOfType<CardHolder>();
    }
}
