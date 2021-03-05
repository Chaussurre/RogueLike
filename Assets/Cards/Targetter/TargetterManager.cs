using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetterManager : MonoBehaviour
{
    [SerializeField]
    Arrow ArrowPrefab;
    readonly List<Arrow> Arrows = new List<Arrow>();
    readonly List<Targetable> Targets = new List<Targetable>();

    public void CreateArrow(Targetable target)
    {
        if (Arrows.Count != 0)
            Targets.Add(target);

        Arrows.Add(Instantiate(ArrowPrefab));
    }

    private void Update()
    {
        if (Arrows.Count == 0)
            return;

        for (int i = 0; i < Targets.Count; i++)
            Arrows[i].DrawStraigth(Vector2.zero, Targets[i].GetScreenPosition());

        Vector2 Position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Arrows[Arrows.Count - 1].DrawCurve(Vector2.zero, Position);
    }

    public void StopTargetting()
    {
        Arrows.ForEach((Arrow arrow) => { Destroy(arrow.gameObject); });    
        Arrows.Clear();
        Targets.Clear();
    }
}
