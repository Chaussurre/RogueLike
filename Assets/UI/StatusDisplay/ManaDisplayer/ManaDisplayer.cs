using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaDisplayer : MonoBehaviour
{
    Text text;

    public void Start()
    {
        text = GetComponentInChildren<Text>();
    }


    private void Update()
    {
        Status status = CombatManager.Instance.GoodGuy.Status;
        text.text = "" + status.Mana + "/" + status.characteristics.Mana;
    }
}
