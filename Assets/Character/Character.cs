using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Status))]
public class Character : MonoBehaviour
{
    public Status Status { get; private set; }

    private void Start()
    {
        Status = GetComponent<Status>();
    }
}

