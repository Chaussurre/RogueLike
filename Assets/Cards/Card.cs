using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public string name;
    public CardBody body { get; private set; } = null;

    private readonly List<CardEffect> Effects = new List<CardEffect>{};
    private void Start()
    {
        transform.parent = CardManager.Instance.transform;
        Effects.AddRange(GetComponents<CardEffect>());
    }

    public void Play()
    {
        foreach (CardEffect e in Effects)
            e.Play();
    }

    public void Discard()
    {
        if (body != null)
            body.Discard();
    }

    public CardBody CreateBody()
    {
        body = Instantiate(CardManager.Instance.BodyPrefab, transform);
        return body;
    }

    public void ResetBody()
    {
        if (body == null)
            return;

        GameObject.Destroy(body.gameObject);
        body = null;
    }
}
