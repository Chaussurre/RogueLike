using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackDisplayer : MonoBehaviour
{
    private Text text;

    private void Start()
    {
        text = GetComponentInChildren<Text>();
    }

    public void DisplayAttack(int attack)
    {
        text.text = attack.ToString();
    }
}
