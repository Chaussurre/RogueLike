using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class CardEffect : MonoBehaviour
{
    protected Targetter Targetter = null;
    Card Card;
    private void Start()
    {
        Targetter = GetComponent<Targetter>();
    }

    public virtual bool CanPlay()
    {
        return true;
    }

    public virtual bool RequireTarget()
    {
        return Targetter != null || Targetter.Target != null;
    }

    public abstract void Play();
}
