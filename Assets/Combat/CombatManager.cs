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

    public TeamManager TeamManager { get; private set; }
    public CardManager CardManager { get; private set; }
    public GameEventManager EventManager { get; private set; }
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
        EventManager = FindObjectOfType<GameEventManager>();
    }

    private void FixedUpdate()
    {
        if (EventManager.IsEmpty())
        {
            TeamManager.TryToPlay(Time.fixedDeltaTime);
            Player.Status.ManaRegen(Time.fixedDeltaTime);
        }
        else
            EventManager.Wait(Time.fixedDeltaTime);
    }

    public void CreateMarker(Character character)
    {
        TimelineMarker marker = Instantiate(MarkerPrefab, TimeLine.transform);
        marker.Init(character);
    }
}
