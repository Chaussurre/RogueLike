using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (LaneManager))]
public class BattleField : MonoBehaviour
{
    public GameObject Projection;
    public List<Lane> Lanes { get; private set; } = new List<Lane>();
    public Lane FrontLine { get; private set; }
    
    public void AddCharacter(Character character)
    {
        foreach (Lane l in Lanes)
            if (l.AddCharacter(character))
                return;

        //All lanes full
        CreateLane().AddCharacter(character);
    }

    public void RemoveCharacter(Character character)
    {
        foreach (Lane l in Lanes)
            if (l.HasCharacter(character, out _))
            {
                l.RemoveCharacter(character);
                if (l.isEmpty())
                    DestroyLane(l);
                break;
            }
    }

    private Lane CreateLane()
    {
        FrontLine = Instantiate(CombatManager.Instance.TeamManager.LanePrefab, transform);
        Lanes.Insert(0, FrontLine);
        return FrontLine;
    }

    private void DestroyLane(Lane lane)
    {
        Destroy(lane.gameObject);
        Lanes.Remove(lane);
        if (Lanes.Count > 0)
            FrontLine = Lanes[0];
    }

    public Vector2 FindPosition(Character character)
    {
        foreach (Lane l in Lanes)
            if (l.HasCharacter(character, out Vector2 position))
                return position;

        return Vector2.zero;
    }
}
