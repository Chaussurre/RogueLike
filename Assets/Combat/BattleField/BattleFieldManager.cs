using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleFieldManager : MonoBehaviour
{
    [SerializeField]
    BattleField LeftField;
    [SerializeField]
    BattleField RightField;

    public Lane LanePrefab;
}
