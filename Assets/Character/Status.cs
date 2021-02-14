using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status : MonoBehaviour
{
    [SerializeField]
    int MaxHp;

    public int Hp { get; private set; }

    void Start()
    {
        if (MaxHp == 0)
            Debug.LogWarning("Max Hp is set to 0");

        Hp = MaxHp;
    }

    public void DealDammage(int damage)
    {
        Hp -= damage;

        if (Hp <= 0)
            Kill();
    }

    public void Kill()
    {
        //Arg
    }

    public int GetMaxHp()
    {
        return MaxHp;
    }
}
