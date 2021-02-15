using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public static CombatManager Instance = null;

    public Character GoodGuy;
    public Character BadGuy;

    public Character TurnPlaying { get; private set; } = null;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else 
            Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        if (TurnPlaying != null)
            return;

        TryPlayTurn(GoodGuy);
        TryPlayTurn(BadGuy);
    }

    private void TryPlayTurn(Character character)
    {
        if (TurnPlaying == null)
            if (character.IsItMyTurn(Time.fixedDeltaTime))
            {
                TurnPlaying = character;
                character.StartTurn();
            }
    }

    public void EndTurn(Character character)
    {
        if (character == TurnPlaying)
            TurnPlaying = null;
    }
}
