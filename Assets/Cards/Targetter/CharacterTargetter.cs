using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterTargetter : Targetter<Character>
{
    Team Team;

    public CharacterTargetter(Character Source, Team Team) : base(Source)
    {
        this.Team = Team;
    }

    public override void AutoTarget(List<Character> Targets)
    {
        foreach (Character character in Team.members)
            Targets.Insert(Random.Range(0, Targets.Count + 1), character);
    }

    protected override bool TargetAtPosition(Vector2 mousePosisition, List<Character> Targets)
    {
        Vector2 delta = Team.Camera.transform.position - Camera.main.transform.position;
        mousePosisition += delta + Team.GetComponent<LaneManager>().GetOffset();

        Debug.DrawLine(Vector3.zero, mousePosisition, Color.blue);

        foreach (Character character in Team.members)
            if (character.body != null && character.body.TryGetComponent(out TargetComponent target)) //Character has a valid body
                if (target.IsOnPosition(mousePosisition))
                {
                    Debug.Log("found : " + character);
                    Targets.Add(character);
                    return true;
                }

        return false;
    }
}
