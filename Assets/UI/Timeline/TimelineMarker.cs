using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimelineMarker : MonoBehaviour
{
    Character character = null;

    private void Update()
    {
        if (character == null)
            return;

        Vector3 Start = Timeline.Instance.StartPoint.position;
        Vector3 End = Timeline.Instance.EndPoint.position;

        float delta = character.TimerUntilPlay / character.Characteristics.TurnFrenquency();
        Debug.Log(delta);
        transform.position = Vector2.Lerp(End, Start, delta);
    }

    public void Init(Character character)
    {
        this.character = character;
    }
}
