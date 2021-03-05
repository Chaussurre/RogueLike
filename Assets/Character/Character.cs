using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Status))]
public class Character : MonoBehaviour, Targetable
{
    public Status Status { get; private set; }
    public Characteristics Characteristics { get; private set; }
    public CharacterBody body { get; private set; }

    private List<CardEffect> Abilities = new List<CardEffect>();

    public Team Team;

    public float TimerUntilPlay { get; private set; }

    protected virtual void Start()
    {
        Status = GetComponent<Status>();
        Characteristics = GetComponent<Characteristics>();
        body = GetComponentInChildren<CharacterBody>();
        TimerUntilPlay = Characteristics.TurnFrenquency();
        CombatManager.Instance.CreateMarker(this);
        Abilities.AddRange(GetComponents<CardEffect>());
    }

    public void TryTurn(float timer)
    {
        TimerUntilPlay -= timer;
        if (TimerUntilPlay < 0)
        {
            PlayTurn();
            TimerUntilPlay += Characteristics.TurnFrenquency();
        }
    }

    protected virtual void PlayTurn()
    {
        CombatManager.Instance.EventManager.Push(new GameEventEndTurn(this));
        Attack();
        PlayEffect();
        CombatManager.Instance.EventManager.Push(new GameEventStartTurn(this));
    }

    virtual protected void PlayEffect()
    {
        if (Abilities.Count > 0)
        {
            CardEffect Ability = Abilities[Random.Range(0, Abilities.Count)];
            CombatManager.Instance.EventManager.Push(new GameEventPlayEffect(this, Ability));
            Ability.OnStack(this);
        }
    }

    public void Attack()
    {
        if (Characteristics.Attack == 0)
            return;

        Team TargetTeam = CombatManager.Instance.TeamManager.GetOpposingTeam(Team);
        List<Character> Targets = TargetTeam.BattleField.FrontLine.Characters;
        Character Target = Targets[Random.Range(0, Targets.Count)];
        CombatManager.Instance.EventManager.Push(new GameEventStrike(this, Target));
    }

    public Vector2 GetScreenPosition()
    {
        return body.GetScreenPosition();
    }
}

