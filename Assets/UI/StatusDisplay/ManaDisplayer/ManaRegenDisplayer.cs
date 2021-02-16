using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaRegenDisplayer : MonoBehaviour
{
    Text text;

    public void Start()
    {
        text = GetComponentInChildren<Text>();
    }


    private void Update()
    {
        Status status = PlayerCharacter.Instance.Status;
        text.text = "+" + status.characteristics.ManaRegen;
    }
}
