﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Card : MonoBehaviour
{
    public string Name;

    private Vector2 TargetPosition = Vector2.zero;
    private SpriteRenderer renderer;
    private Collider2D collider;
    private bool Grabbed = false;

    private void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        renderer.color = Random.ColorHSV(0, 1, 0, 1, 0, 1, 1, 1);
        collider = GetComponent<Collider2D>();
    }
    void Update()
    {
        GrabbingUpdate();

        if (!isOnTargetPosition())
            MoveToTarget();
    }

    bool isOnTargetPosition()
    {
        if (Vector2.Distance(TargetPosition, transform.position) < 0.001f) //Arbitrary epsilon value
        {
            transform.position = TargetPosition;
            return true;
        }
        return false;
    }

    void MoveToTarget()
    {
        float Velocity = 10f;
        transform.position = Vector2.LerpUnclamped(transform.position, TargetPosition, Velocity * Time.deltaTime);
    }

    void GrabbingUpdate()
    {
        if (IsHovered() && Input.GetMouseButtonDown(0))
            Grabbed = true;
        if (!Input.GetMouseButton(0))
            Grabbed = false;

        if (Grabbed)
            SetPosition(Camera.main.ScreenToWorldPoint(Input.mousePosition));
    }

    public void SetPosition(Vector2 position)
    {
        TargetPosition = position;
    }

    public void SetPriority(int priority)
    {
        renderer.sortingOrder = priority;
    }

    public bool IsHovered()
    {
        Vector2 MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (collider.OverlapPoint(MousePos))
            return true;

        Vector2 closest = collider.ClosestPoint(MousePos);
        if (Mathf.Abs(closest.x - MousePos.x) < 0.1f && closest.y >= MousePos.y)
            return true; //Mouse is under the card

        return false;
    }

    public bool IsGrabbed()
    {
        return Grabbed;
    }
}
