﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Characteristics)), 
    RequireComponent(typeof(StatusDisplay)), 
    RequireComponent(typeof(StatusAlterationManager))]
public class Status : MonoBehaviour
{
    public Characteristics characteristics { get; private set; }
    public StatusAlterationManager StatusAlteration { get; private set; }
    public Character Character { get; private set; }
    public int Hp { get; private set; }
    public int Mana { get; private set; }

    private float TimerRegenMana = 0;

    void Start()
    {
        characteristics = GetComponent<Characteristics>();
        StatusAlteration = GetComponent<StatusAlterationManager>();

        if (characteristics.Hp == 0)
            Debug.LogWarning("Max Hp is set to 0");

        Character = GetComponent<Character>();

        Hp = characteristics.Hp;
        Mana = characteristics.Mana;
    }

    public void DealDammage(int damage)
    {
        if (damage < 0)
            damage = 0;

        Hp -= damage;

        if (Hp <= 0)
        {
            Hp = 0;
            Kill();
        }
    }

    public void Heal(int Heal)
    {
        Hp += Heal;

        if (Hp > characteristics.Hp)
            Hp = characteristics.Hp;
    }

    public void PayMana(int cost)
    {
        Mana -= cost;
        if (Mana < 0)
            Mana = 0;
    }

    public void ManaRegen(float timer)
    {
        if (Mana >= characteristics.Mana)
        {
            TimerRegenMana = 0;
            return;
        }

        TimerRegenMana += timer;
        if (TimerRegenMana > characteristics.ManaRegenFrequency())
        {
            TimerRegenMana = 0;
            Mana++;
        }
    }

    public void IncreaseMana(int amount)
    {
        Mana = Mathf.Min(Mana + amount, characteristics.Mana);
    }

    public float percentManaRegen()
    {
        return TimerRegenMana / characteristics.Mana * 100f;
    }

    public virtual void Kill()
    {
        Character.body.DestroyMarker();
        Character.Team.RemoveCharacter(Character);
        Destroy(Character.gameObject);
    }
}
