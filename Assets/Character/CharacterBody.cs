using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBody : MonoBehaviour
{
    bool animating = false; //performing an animation
    Character Character;
    public TimelineMarker marker { get; private set; }

    private void Start()
    {
        Character = GetComponentInParent<Character>();
    }

    public void SetColor(Color color)
    {
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        renderer.color = color;
    }

    public void Strike()
    {
        Vector2 targetDirection = Vector2.left;
        if (Character.Team.IsLeft)
            targetDirection = Vector2.right;

        StartCoroutine("StrikeRoutine", targetDirection.normalized);
    }

    private void Update()
    {
        if (!animating)
            SetToPosition();
    }

    private void SetToPosition()
    {
        Vector2 pos = Character.Team.BattleField.FindPosition(Character);
        transform.position = pos;
    }

    public void SetMarker(TimelineMarker marker)
    {
        this.marker = marker;
    }

    public void DestroyMarker()
    {
        if(marker != null)
        Destroy(marker.gameObject);
    }

    IEnumerator StrikeRoutine(Vector2 direction)
    {
        animating = true;
        Vector2 origin = transform.position;
        float TimerMax = 0.1f;
        float amplitude = 1f;
        float Timer = 0;

        while (Timer < TimerMax / 3) //strike
        {
            Timer += Time.fixedDeltaTime;
            transform.position = origin + direction * (Timer * 3f / TimerMax) * amplitude;
            yield return new WaitForFixedUpdate();
        }

        Timer = 0;

        while(Timer < TimerMax)//recoil
        {
            Timer += Time.fixedDeltaTime;
            transform.position = origin + direction * (1 - Timer / TimerMax) * amplitude;
            yield return new WaitForFixedUpdate();
        }

        transform.position = origin;
        animating = false;
    }
}
