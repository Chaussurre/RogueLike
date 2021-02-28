using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamManager : MonoBehaviour
{
    [SerializeField]
    Team LeftTeam;
    [SerializeField]
    Team RightTeam;

    public Lane LanePrefab;
    
    private void Start()
    {
        AddPlayer(CombatManager.Instance.Player);
        AddBadGuy(CombatManager.Instance.BadGuy);
    }

    public void AddPlayer(PlayerCharacter player)
    {
        LeftTeam.AddCharacter(player);
    }

    public void AddBadGuy(Character BadGuy)
    {
        RightTeam.AddCharacter(BadGuy);
    }

    public void AddCharacter(Character character, Team team)
    {
        team.AddCharacter(character);
    }

    public void TryToPlay(float timer)
    {
        LeftTeam.TryToPlay(timer);
        RightTeam.TryToPlay(timer);
        return;
    }

    public Team GetOpposingTeam(Team team)
    {
        if (team == LeftTeam)
            return RightTeam;
        return LeftTeam;
    }
}
