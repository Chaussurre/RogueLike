using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Characteristics : MonoBehaviour
{
    public int Hp;
    public int Attack;
    public int Speed;
    public int Mana;
    public int ManaRegen;

    public float TurnFrenquency()
    {
        return 1000f / Speed;
    }

    public float ManaRegenFrequency()
    {
        return 1000f / ManaRegen;
    }
}