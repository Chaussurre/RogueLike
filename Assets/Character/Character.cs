using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Status))]
public class Character : MonoBehaviour
{
    public Status Status { get; private set; }
    public Characteristics Characteristics { get; private set; }
    public CharacterBody body { get; private set; }

    public Team Team;

    public float TimerUntilPlay { get; private set; }

    protected virtual void Start()
    {
        Status = GetComponent<Status>();
        Characteristics = GetComponent<Characteristics>();
        body = GetComponentInChildren<CharacterBody>();
        TimerUntilPlay = Characteristics.TurnFrenquency();
        CombatManager.Instance.CreateMarker(this);
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
        Team TargetTeam = CombatManager.Instance.TeamManager.GetOpposingTeam(Team);
        List<Character> Targets = TargetTeam.BattleField.FrontLine.Characters;
        Character Target = Targets[Random.Range(0, Targets.Count)];
        Target.Status.DealDammage(Characteristics.Attack);
        body.Strike(Target);
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

