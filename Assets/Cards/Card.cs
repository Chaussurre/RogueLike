﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public string name;
    [TextArea]
    public string description;
    public CardBody body { get; private set; } = null;

    private readonly List<CardEffect> Effects = new List<CardEffect>{};
    private void Start()
    {
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

    public CardBody CreateBody(Vector2 position)
    {
        body = Instantiate(CardManager.Instance.BodyPrefab, position, Quaternion.identity, transform);
        body.SetNameDescription(name, description);
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
