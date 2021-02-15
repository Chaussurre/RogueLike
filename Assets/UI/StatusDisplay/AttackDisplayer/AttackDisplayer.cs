using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackDisplayer : MonoBehaviour
{
    public void DisplayAttack(int attack)
    {
        Text text = GetComponentInChildren<Text>();
        text.text = attack.ToString();
    }
}
