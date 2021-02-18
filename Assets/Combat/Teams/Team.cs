﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BattleField))]
public class Team : MonoBehaviour
{
    public bool IsLeft;

    public BattleField BattleField { get; private set; }

    private readonly HashSet<Character> members = new HashSet<Character>();

    private void Start()
    {
        BattleField = GetComponent<BattleField>();
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
        //BattleField.RemoveCharacter(character);
    }

    public Character TryToPlay(float timer)
    {
        foreach (Character c in members)
            if(c.IsItMyTurn(timer))
                return c;
        return null;
    }
}
