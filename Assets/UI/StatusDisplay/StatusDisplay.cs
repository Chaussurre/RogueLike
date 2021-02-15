using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusDisplay : MonoBehaviour
{
    AttackDisplayer attack;
    HealthBar health;
    Status status; 
    private void Start()
    {
        attack = GetComponentInChildren<AttackDisplayer>();
        health = GetComponentInChildren<HealthBar>();
        status = GetComponent<Status>();
    }

    private void Update()
    {
        attack.DisplayAttack(status.characteristics.Attack);
        health.SetMaxLife(status.characteristics.Hp);
        health.SetLife(status.Hp);
    }
}
