using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lane : MonoBehaviour
{
    public List<Transform> Positions;

    [HideInInspector]
    public List<Character> Characters;

    public bool isFull()
    {
        return Positions.Count <= Characters.Count;
    }

    public bool AddCharacter(Character character)
    {
        if (isFull())
            return false;

        Characters.Add(character);
        return true;
    }

    public void RemoveCharacter(Character character)
    {
        Characters.Remove(character);
    }

    public bool HasCharacter(Character character, out Vector2 position)
    {
        for(int i = 0; i < Characters.Count; i++)
            if (Characters[i] == character)
            {
                position = Positions[i].position;
                return true;
            }

        position = Vector2.zero;
        return false;
    }
}
