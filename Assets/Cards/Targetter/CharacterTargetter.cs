using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterTargetter : Targetter<Character>
{
    bool TeamLeft;
    bool TeamRight;
    TeamManager TeamManager;
    public CharacterTargetter(Character Source, Team Team, int NbTargets = 1) : base(Source, NbTargets)
    {
        TeamManager = CombatManager.Instance.TeamManager;
        if (Team == TeamManager.LeftTeam || Team == null)
        {
            TeamLeft = true;
            foreach (Character member in TeamManager.LeftTeam.members)
                Candidates.Add(member);
        }
        else
            TeamLeft = false;

        if (Team == TeamManager.RightTeam || Team == null)
        {
            foreach (Character member in TeamManager.RightTeam.members)
                Candidates.Add(member);
            TeamRight = true;
        }
        else
            TeamRight = false;
    }

    public override void AutoTarget(List<Character> Targets)
    {
        List<Character> Tmp = new List<Character>();
        foreach (Character character in Candidates)
            Tmp.Insert(Random.Range(0, Tmp.Count + 1), character);

        int MaxNbTargets = Mathf.Min(NbTargets, Tmp.Count);
        Targets.AddRange(Tmp.GetRange(0,  MaxNbTargets));
    }

    protected override bool TargetAtPosition(Vector2 mousePosition, List<Character> Targets)
    {
        if (TeamLeft && TargetTeam(mousePosition, Targets, TeamManager.LeftTeam))
            return true;
        if (TeamRight && TargetTeam(mousePosition, Targets, TeamManager.RightTeam))
            return true;
        return false;
    }

    bool TargetTeam(Vector2 mousePosition, List<Character> Targets, Team team)
    {
        if (!team.BattleField.GetComponent<BoxCollider2D>().OverlapPoint(mousePosition))
            return false;

        Vector2 delta = team.Camera.transform.position - Camera.main.transform.position;
        mousePosition += delta + team.GetComponent<LaneManager>().GetOffset();

        Debug.DrawLine(Vector3.zero, mousePosition, Color.blue, Time.deltaTime);

        foreach (Character character in team.members)
        {
            if (!Candidates.Contains(character))
                continue;

            if (Targets.Contains(character))
                continue;
            
            if (character.body == null || !character.body.TryGetComponent(out TargetComponent target)) //Character has a valid body
                continue;

            if (target.IsOnPosition(mousePosition))
            {
                Targets.Add(character);
                return true;
            }
        }

        return false;
    }
}
