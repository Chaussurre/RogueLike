﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaDisplayer : MonoBehaviour
{
    SpriteMask mask;
    Text text;

    public void Start()
    {
        text = GetComponentInChildren<Text>();
        mask = GetComponentInChildren<SpriteMask>();    
    }

    private void Update()
    {
        Status status = PlayerCharacter.Instance.Status;
        text.text = "" + status.Mana + "/" + status.characteristics.Mana;

        mask.transform.localPosition = Vector2.up * status.percentManaRegen() / 100f * 4;
    }
}
