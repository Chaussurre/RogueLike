using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBody : MonoBehaviour
{
    public void Strike(Character target)
    {
        Vector2 targetDirection = target.transform.position - transform.position;
        StartCoroutine("StrikeRoutine", targetDirection.normalized);
    }

    IEnumerator StrikeRoutine(Vector2 direction)
    {
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
    }
}
