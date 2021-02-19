using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Characteristics)), RequireComponent(typeof(StatusDisplay))]
public class Status : MonoBehaviour
{
    public Characteristics characteristics { get; private set; }
    public Character Character { get; private set; }
    public int Hp { get; private set; }
    public int Mana { get; private set; }

    private float TimerRegenMana = 0;

    void Start()
    {
        characteristics = GetComponent<Characteristics>();

        if (characteristics.Hp == 0)
            Debug.LogWarning("Max Hp is set to 0");

        Character = GetComponent<Character>();

        Hp = characteristics.Hp;
        Mana = characteristics.Mana;
    }

    private void Update()
    {
        if (CombatManager.Instance.isWaiting())
            ManaRegen(Time.deltaTime);
    }

    public void DealDammage(int damage)
    {
        Hp -= damage;

        if (Hp <= 0)
        {
            Hp = 0;
            Kill();
        }
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

    public float percentManaRegen()
    {
        return TimerRegenMana / characteristics.Mana * 100f;
    }

    public void Kill()
    {
        Character.body.DestroyMarker();
        Character.Team.RemoveCharacter(Character);
        Destroy(Character.gameObject);
    }
}
