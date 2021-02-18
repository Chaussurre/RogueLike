using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TeamManager)),
    RequireComponent(typeof(CardManager))]
public class CombatManager : MonoBehaviour
{
    public static CombatManager Instance = null;

    public PlayerCharacter Player;
    public Character BadGuy;

    public TimelineMarker MarkerPrefab;

    public Character TurnPlaying { get; private set; } = null;
    public TeamManager TeamManager { get; private set; }
    public CardManager CardManager { get; private set; }
    public Timeline TimeLine { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else 
            Destroy(gameObject);

        TeamManager = GetComponent<TeamManager>();
        CardManager = GetComponent<CardManager>();
        TimeLine = FindObjectOfType<Timeline>();
    }

    private void FixedUpdate()
    {
        if (TurnPlaying != null)
            return;

        TurnPlaying = TeamManager.TryToPlay(Time.fixedDeltaTime);
        if (TurnPlaying != null) 
            TurnPlaying.StartTurn();
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

    public bool isWaiting()
    {
        return TurnPlaying == null;
    }
}
