using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class CardEffect : MonoBehaviour
{
    public virtual bool CanPlay()
    {
        return true;
    }

    public virtual void OnStack(Character caster) { }

    public abstract void Play(Character caster);
}
