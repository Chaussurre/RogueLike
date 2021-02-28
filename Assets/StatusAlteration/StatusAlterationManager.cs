using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusAlterationManager : MonoBehaviour
{
    [HideInInspector]
    public Status Status;

    List<StatusAlteration> Alterations = new List<StatusAlteration>();

    private void Start()
    {
        Status = GetComponent<Status>();
    }
    private void FixedUpdate()
    {
        foreach (StatusAlteration alt in Alterations)
            alt.TryEffects(Time.fixedDeltaTime);
    }

    public void AddAlteration(StatusAlteration AlterationPrefab)
    {
        foreach (StatusAlteration alt in Alterations)
            if (alt.Compare(AlterationPrefab))
            {
                if (alt.IsStackable)
                    alt.AddStacks(AlterationPrefab);
                return;
            }

        Alterations.Add(Instantiate(AlterationPrefab, transform));
    }
    
    public void RemoveAlteration(string name)
    {
        foreach (StatusAlteration alt in Alterations)
            if (alt.Name == name)
                Alterations.Remove(alt);
    }
    
    public void RemoveAlteration(StatusAlteration alteration)
    {
        Alterations.Remove(alteration);
    }

    public bool AllowPlay(Card card)
    {
        foreach (StatusAlteration alt in Alterations)
            if (!alt.AllowPlay(card))
                return false;

        return true;
    }
}
