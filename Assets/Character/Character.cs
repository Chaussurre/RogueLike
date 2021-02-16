using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Status))]
public class Character : MonoBehaviour
{

    [SerializeField]
    Color color;
    public Status Status { get; private set; }
    public Characteristics Characteristics { get; private set; }
    CharacterBody body;

    public float TimerUntilPlay { get; private set; }

    private void Start()
    {
        Status = GetComponent<Status>();
        Characteristics = GetComponent<Characteristics>();
        body = GetComponentInChildren<CharacterBody>();
        TimerUntilPlay = Characteristics.TurnFrenquency();
        CombatManager.Instance.CreateMarker(this);
    }

    public bool IsItMyTurn(float timer)
    {
        Status.ManaRegen(timer);
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
        Character Target = CombatManager.Instance.GetOpponent(this);
        Target.Status.DealDammage(Characteristics.Attack);
        body.Strike(CombatManager.Instance.GetOpponent(this));
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

