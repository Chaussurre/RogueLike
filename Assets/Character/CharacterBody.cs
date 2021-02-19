using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBody : MonoBehaviour
{
    bool animating = false; //performing an animation
    Character Character;

    private void Start()
    {
        Character = GetComponentInParent<Character>();
    }

    public void SetColor(Color color)
    {
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        renderer.color = color;
    }

    public void SetLayer(string layer)
    {
        GetComponent<SpriteRenderer>().sortingLayerName = layer;
    }

    public void Strike(Character target)
    {
        Vector2 targetDirection = target.body.transform.position - transform.position;
        StartCoroutine("StrikeRoutine", targetDirection.normalized);
    }

    private void Update()
    {
        SetLayer(Character.Team.BattleField.Layer);
        if (!animating)
            SetToPosition();
    }

    private void SetToPosition()
    {
        Vector2 pos = Character.Team.BattleField.FindPosition(Character);
        transform.position = pos;
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
