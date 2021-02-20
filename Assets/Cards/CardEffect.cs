using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class CardEffect : MonoBehaviour
{
    public virtual bool CanPlay()
    {
        return true;
    }

    public abstract void Play();
}
