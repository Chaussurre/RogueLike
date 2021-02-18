using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BattleFieldManager)),
    RequireComponent(typeof(CardManager))]
public class CombatManager : MonoBehaviour
{
    public static CombatManager Instance = null;

    public PlayerCharacter Player;
    public Character BadGuy;

    public TimelineMarker MarkerPrefab;

    public Character TurnPlaying { get; private set; } = null;
    public BattleFieldManager BattleFieldManager { get; private set; }
    public CardManager CardManager { get; private set; }
    public Timeline TimeLine { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else 
            Destroy(gameObject);

        BattleFieldManager = GetComponent<BattleFieldManager>();
        CardManager = GetComponent<CardManager>();
        TimeLine = FindObjectOfType<Timeline>();
    }

    private void FixedUpdate()
    {
        if (TurnPlaying != null)
            return; 

        TryPlayTurn(Player);
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

    public void CreateMarker(Character character)
    {
        TimelineMarker marker = Instantiate(MarkerPrefab, TimeLine.transform);
        marker.Init(character);
    }

    public Character GetOpponent(Character me)
    {
        if (me == Player)
            return BadGuy;
        return Player;
    }

    public bool isWaiting()
    {
        return TurnPlaying == null;
    }
}
