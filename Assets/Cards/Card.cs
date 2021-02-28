using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour, Targetable
{
    public string Name;
    public int ManaCost = 1;
    public int Stamina = 1;
    GameEventManager EventManager;
    [TextArea]
    public string description;
    public CardBody body { get; private set; } = null;
    private CardManager CardManager;

    private List<CardEffect> Effects = new List<CardEffect>{};
    private void Awake()
    {
        CardManager = CombatManager.Instance.CardManager;
        EventManager = CombatManager.Instance.EventManager;
        Effects.AddRange(GetComponentsInChildren<CardEffect>());
        Effects.Reverse();
    }

    public bool CanPlay()
    {
        foreach (CardEffect e in Effects)
            if (!e.CanPlay())
                return false;
        return true;
    }

    public void Play(Character caster)
    {
        StaminaDecrease();
        foreach (CardEffect e in Effects)
            EventManager.Push(new GameEventPlayEffect(caster, e));
        foreach (CardEffect e in Effects)
            e.OnStack(caster);
    }

    private void StaminaDecrease()
    {
        if (Stamina == 0)
            EventManager.Push(new GameEventDiscard(this));
        else
        {
            Stamina--;
            EventManager.Push(new GameEventPlayCardShuffle(this));
        }
    }

    public void Discard()
    {
        CombatManager.Instance.EventManager.Push(new GameEventDiscard(this));
    }

    public void Reshuffle()
    {
        CombatManager.Instance.EventManager.Push(new GameEventPlayCardShuffle(this));
    }

    public CardBody CreateBody(Vector2 position)
    {
        body = Instantiate(CardManager.BodyPrefab, position, Quaternion.identity, transform);
        body.Innit(this);
        return body;
    }

    public void ResetBody()
    {
        if (body == null)
            return;

        Destroy(body.gameObject);
        body = null;
    }
}
