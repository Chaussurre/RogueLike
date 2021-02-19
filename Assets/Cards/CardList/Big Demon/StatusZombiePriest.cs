using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusZombiePriest : Status
{
    bool revivedOnce = false;

    public override void Kill()
    {
        if (!revivedOnce)
        {
            revivedOnce = true;
            Heal(characteristics.Hp);
        }
        else
            base.Kill();

    }
}
