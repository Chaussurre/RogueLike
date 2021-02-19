using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(BoxCollider2D))]
public class OnBattleField : MonoBehaviour
{
    BoxCollider2D box;
    List<Canvas> CanvasList = new List<Canvas>();

    void Start()
    {
        box = GetComponent<BoxCollider2D>();
        CanvasList.AddRange(GetComponentsInChildren<Canvas>());
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Canvas c in CanvasList)
        {
        }
    }
}
