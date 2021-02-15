using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Status))]
public class Character : MonoBehaviour
{
    public Status Status { get; private set; }
    public Characteristics Characteristics { get; private set; }

    float TimerUntilPlay;

    private void Start()
    {
        Status = GetComponent<Status>();
        Characteristics = GetComponent<Characteristics>();
        TimerUntilPlay = Characteristics.TurnFrenquency();
    }

    public bool IsItMyTurn(float timer)
    {
        TimerUntilPlay -= timer;
        if (TimerUntilPlay < 0)
        {
            TimerUntilPlay += Characteristics.TurnFrenquency();
            return true;
        }
        return false;
    }

    public virtual void StartTurn()
    {
        StartCoroutine("TurnRoutine");
    }

    virtual protected bool PlayEffect()
    {
        Debug.Log(ToString() + " play an effect!");
        return true;
    }

    private void Attack()
    {
        Debug.Log(ToString() + " attack!");
    }

    private void EndTurn()
    {
        CombatManager.Instance.EndTurn(this);
    }

    IEnumerator TurnRoutine()
    {
        while (!PlayEffect())
            yield return new WaitForFixedUpdate();
        Attack();
        yield return new WaitForSeconds(0.5f);
        EndTurn();
    }
}

