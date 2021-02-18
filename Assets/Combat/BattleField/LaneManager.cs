using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaneManager : MonoBehaviour
{
    public Transform StartingPoint;
    public float LaneDistance;

    BattleField BattleField;

    private void Start()
    {
        BattleField = GetComponent<BattleField>();
    }

    private void Update()
    {
        Vector2 position = StartingPoint.position;
        Vector2 delta = Vector2.right * LaneDistance;

        if (BattleField.IsLeft)
            delta *= -1;

        foreach(Lane l in BattleField.Lanes)
        {
            l.transform.position = position;
            position += delta;
        }
    }
}
