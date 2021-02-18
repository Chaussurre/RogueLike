using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaneManager : MonoBehaviour
{
    public Transform StartingPoint;
    public float LaneDistance;

    Team Team;

    private void Start()
    {
        Team = GetComponent<Team>();
    }

    private void Update()
    {
        Vector2 position = StartingPoint.position;
        Vector2 delta = Vector2.right * LaneDistance;

        if (Team.IsLeft)
            delta *= -1;

        foreach(Lane l in Team.BattleField.Lanes)
        {
            l.transform.position = position;
            position += delta;
        }
    }
}
