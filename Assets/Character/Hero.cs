using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : Character
{
    public static Hero Instance;

    public bool CanPlay { get; private set; } = false; //When the player decide he played all his card

    private void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);
        else
            Instance = this;
    }

    public override void StartTurn()
    {
        CanPlay = true;
        CardHolder.Instance.Draw();
        base.StartTurn();
    }

    protected override bool PlayEffect()
    {
        return !CanPlay;
    }

    public void StopPlayingCards()
    {
        CanPlay = false;
    }
}
