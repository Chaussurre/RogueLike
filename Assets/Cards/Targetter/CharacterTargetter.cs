using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterTargetter : Targetter<Character>
{
    Team Team;

    public CharacterTargetter(Character Source, Team Team, int NbTargets = 1) : base(Source, NbTargets)
    {
        this.Team = Team;
    }

    public override void AutoTarget(List<Character> Targets)
    {
        List<Character> Tmp = new List<Character>();
        foreach (Character character in Team.members)
            Tmp.Insert(Random.Range(0, Targets.Count + 1), character);

        Targets.AddRange(Tmp.GetRange(0, NbTargets));
    }

    protected override bool TargetAtPosition(Vector2 mousePosisition, List<Character> Targets)
    {
        Vector2 delta = Team.Camera.transform.position - Camera.main.transform.position;
        mousePosisition += delta + Team.GetComponent<LaneManager>().GetOffset();

        Debug.DrawLine(Vector3.zero, mousePosisition, Color.blue, Time.deltaTime);

        foreach (Character character in Team.members)
            if (character.body != null && character.body.TryGetComponent(out TargetComponent target)) //Character has a valid body
                if (target.IsOnPosition(mousePosisition))
                {
                    Targets.Add(character);
                    return true;
                }

        return false;
    }
}
