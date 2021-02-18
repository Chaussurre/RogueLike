using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (LaneManager))]
public class BattleField : MonoBehaviour
{
    public List<Lane> Lanes { get; private set; } = new List<Lane>();
    public Lane FrontLine { get; private set; }
    public bool IsLeft;

    public void AddCharacter(Character character)
    {
        foreach (Lane l in Lanes)
            if (l.AddCharacter(character))
                return;

        //All lanes full
        CreateLane().AddCharacter(character);
    }
    private Lane CreateLane()
    {
        FrontLine = Instantiate(CombatManager.Instance.BattleFieldManager.LanePrefab, transform);
        Lanes.Add(FrontLine);
        return FrontLine;
    }

    public bool HasCharacter(Character character, out Vector2 position)
    {
        foreach (Lane l in Lanes)
            if (l.HasCharacter(character, out position))
                return true;

        position = Vector2.zero;
        return false;
    }
}
