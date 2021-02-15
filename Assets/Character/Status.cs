using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Characteristics)), RequireComponent(typeof(StatusDisplay))]
public class Status : MonoBehaviour
{
    public Characteristics characteristics { get; private set; }
    public int Hp { get; private set; }

    void Start()
    {
        characteristics = GetComponent<Characteristics>();

        if (characteristics.Hp == 0)
            Debug.LogWarning("Max Hp is set to 0");

        Hp = characteristics.Hp;
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
}
