using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class LaneManager : MonoBehaviour
{
    public Transform StartingPoint;
    private BoxCollider2D Box;
    private MeshRenderer Projection;
    private Camera Camera;
    public float LaneDistance;

    Team Team;

    private bool Grabbed = false;
    private float GrabbedPosition;
    private void Start()
    {
        Team = GetComponent<Team>();
        Box = GetComponent<BoxCollider2D>();
        Camera = GetComponentInChildren<Camera>();
        Projection = GetComponentInChildren<MeshRenderer>();
    }

    private void Update()
    {
        SetLanePositions();
        if (Grabbed)
        {
            Vector2 MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Camera.transform.position -= Vector3.right * (MousePos.x - GrabbedPosition);
            GrabbedPosition = MousePos.x;
            Grabbed = !Input.GetMouseButtonUp(0);
        }
        else
            Grabbed = TryGrab(out GrabbedPosition);
    }

    private void SetLanePositions()
    {
        Vector2 position = StartingPoint.position;
        Vector2 delta = Vector2.right * LaneDistance;

        if (Team.IsLeft)
            delta *= -1;

        foreach(Lane l in Team.BattleField.Lanes)
        {
            l.transform.position = position;
            position += delta;
        }
    }

    private bool TryGrab(out float Position)
    {
        Vector2 MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Position = MousePos.x;
        return Box.OverlapPoint(MousePos) && Input.GetMouseButtonDown(0);
    }

    public Vector2 GetOffset()
    {
        return -Projection.transform.position;
    }
}