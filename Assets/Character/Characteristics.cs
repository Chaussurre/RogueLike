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
        if (Speed == 0)
            return 0;
        return 1000f / Speed;
    }

    public float ManaRegenFrequency()
    {
        if (ManaRegen == 0)
            return 0;
        return 1000f / ManaRegen;
    }

    public void AddCharacteristics(Characteristics other)
    {
        Hp += other.Hp;
        Attack += other.Attack;
        Speed += other.Speed;
        Mana += other.Mana;
        ManaRegen += other.ManaRegen;
    }
}