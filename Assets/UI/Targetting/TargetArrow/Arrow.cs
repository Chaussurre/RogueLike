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

        List<Vector2> points = GetPointsCurve(Start, End);
        UpdateListBody(points.Count);

        for (int i = 0; i < points.Count; i++)
            ListBody[i].transform.position = points[i];
    }

    List<Vector2> GetPointsCurve(Vector2 Start, Vector2 End)
    {
        if (Start == End)
            return new List<Vector2>();

        List<Vector2> Corners = new List<Vector2>() { Start, End + Vector2.down * CurveSize, End };
        List<Vector2> points = new List<Vector2>();
        float step = DistanceStep;
        for(float distance = 0f; distance < 1f; distance += step)
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