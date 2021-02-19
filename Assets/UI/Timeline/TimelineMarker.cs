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

        Vector3 Start = CombatManager.Instance.TimeLine.StartPoint.position;
        Vector3 End = CombatManager.Instance.TimeLine.EndPoint.position;

        float delta = character.TimerUntilPlay / character.Characteristics.TurnFrenquency();
        transform.position = Vector2.Lerp(End, Start, delta);
    }

    public void Init(Character character)
    {
        this.character = character;
        character.body.SetMarker(this);
        GetComponent<SpriteRenderer>().color = character.body.GetComponent<SpriteRenderer>().color;
    }
}
