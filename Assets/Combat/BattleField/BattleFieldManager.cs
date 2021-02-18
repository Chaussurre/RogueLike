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
    
    private void Start()
    {
        AddPlayer(CombatManager.Instance.Player);
        AddBadGuy(CombatManager.Instance.BadGuy);
    }

    public void AddPlayer(PlayerCharacter player)
    {
        LeftField.AddCharacter(player);
    }

    public void AddBadGuy(Character BadGuy)
    {
        RightField.AddCharacter(BadGuy);
    }

    public Vector2 FindPosistion(Character character)
    {
        if (LeftField.HasCharacter(character, out Vector2 position))
            return position;
        if (RightField.HasCharacter(character, out position))
            return position;
        return Vector2.zero;
    }
}
