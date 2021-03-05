using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField]
    float DistanceStep;
    [SerializeField]
    float CurveSize;
    [SerializeField]
    GameObject HeadPrefab;
    GameObject Head = null;
    [SerializeField]
    GameObject BodyPrefab;

    readonly List<GameObject> ListBody = new List<GameObject>();

    public void DrawStraigth(Vector2 Start, Vector2 End)
    {
        if(Head != null)
        {
            Destroy(Head);
            Head = null;
        }

        int points = Mathf.FloorToInt(Vector2.Distance(Start, End) / DistanceStep);
        UpdateListBody(points);

        for (int i = 0; i < points; i++)
            ListBody[i].transform.position = Vector2.Lerp(Start, End, DistanceStep * i);
    }

    public void DrawCurve(Vector2 Start, Vector2 End)
    {
        if(Head == null)
            Head = Instantiate(HeadPrefab, transform);

        Head.transform.position = End;

        float deltaX = Mathf.Atan(Start.x - End.x);
        float deltaY = Mathf.Atan(Start.y - End.y);
        Vector2 MiddlePoint = End + Vector2.up * deltaY * CurveSize + Vector2.right * deltaX;
        Debug.DrawLine(Start, MiddlePoint, Color.yellow);
        Debug.DrawLine(End, MiddlePoint, Color.yellow);
        List<Vector2> Corners = new List<Vector2>() { Start, MiddlePoint, End };

        AngleHead(End - MiddlePoint);

        List<Vector2> points = GetPointsCurve(Corners);
        UpdateListBody(points.Count);

        for (int i = 0; i < points.Count; i++)
            ListBody[i].transform.position = points[i];
    }

    private void AngleHead(Vector2 delta)
    {
        float Angle = Vector2.SignedAngle(Vector2.up, delta);
        Head.transform.rotation = Quaternion.Euler(0, 0, Angle);
    }

    List<Vector2> GetPointsCurve(List<Vector2> Corners)
    {
        List<Vector2> points = new List<Vector2>();
        float step = DistanceStep;
        for(float distance = 0f; distance < 1f - step; distance += step)
            points.Add(PointOnCurve(Corners, distance));

        return points;
    }

    Vector2 PointOnCurve(List<Vector2> Corners, float position)
    {
        if (Corners.Count == 1)
            return Corners[0];

        List<Vector2> newCorners = new List<Vector2>();

        for (int i = 0; i < Corners.Count - 1; i++)
            newCorners.Add(Vector2.Lerp(Corners[i], Corners[i + 1], position));

        return PointOnCurve(newCorners, position);
    }
    
    void UpdateListBody(int newSize)
    {
        while(ListBody.Count < newSize)
            ListBody.Add(Instantiate(BodyPrefab, transform));

        while(ListBody.Count > newSize)
        {
            GameObject body = ListBody[0];
            ListBody.RemoveAt(0);
            Destroy(body);
        }
    }
}