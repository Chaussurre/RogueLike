﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BattleField))]
public class Team : MonoBehaviour
{
    public bool IsLeft;

    public BattleField BattleField { get; private set; }
    public Camera Camera { get; private set; }

    public readonly HashSet<Character> members = new HashSet<Character>();

    private void Awake()
    {
        BattleField = GetComponent<BattleField>();
        Camera = GetComponentInChildren<Camera>();
    }

    public void AddCharacter(Character character)
    {
        members.Add(character);
        BattleField.AddCharacter(character);
        character.Team = this;
    }

    public void RemoveCharacter(Character character)
    {
        members.Remove(character);
        //TODO
        BattleField.RemoveCharacter(character);
        if (members.Count == 0)
            Loose();
    }

    public void TryToPlay(float timer)
    {
        foreach (Character c in members)
            c.TryTurn(timer);
    }

    public void Loose()
    {
        Debug.Log(ToString() + " lost !");
    }
}
