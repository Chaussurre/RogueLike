using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class BattleFieldBarrier : MonoBehaviour
{
    public static BoxCollider2D box { get; private set; }

    private void Start()
    {
        box = GetComponent<BoxCollider2D>();
    }
}
