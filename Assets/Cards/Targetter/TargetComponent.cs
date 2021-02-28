using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class TargetComponent : MonoBehaviour
{
    Collider2D Collider;
    private void Start()
    {
        Collider = GetComponent<Collider2D>();
    }

    public bool IsOnPosition(Vector2 Position)
    {
        return Collider.OverlapPoint(Position);
    }
}
