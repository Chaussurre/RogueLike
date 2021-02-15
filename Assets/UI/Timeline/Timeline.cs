using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timeline : MonoBehaviour
{
    public static Timeline Instance = null;

    public Transform StartPoint;
    public Transform EndPoint;
    
    void Start()
    {
        if (Instance != null)
            Destroy(gameObject);
        else
            Instance = this;
    }

}
