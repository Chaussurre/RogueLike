using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField]
    int MaxHp;
    int Hp;

    // Start is called before the first frame update
    void Start()
    {
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
}

