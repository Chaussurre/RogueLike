using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StatusAlteration : MonoBehaviour
{
    protected StatusAlterationManager manager;

    public string Name;
    [TextArea]
    public string Description;
    [SerializeField]
    public bool IsStackable;
    [SerializeField]
    float TriggerFrequency;

    float Timer;
    public int Stacks { get; protected set; } = 1;

    private void Start()
    {
        manager = GetComponentInParent<StatusAlterationManager>();
        OnStart();
    }
    public virtual void AddStacks(StatusAlteration otherPrefab)
    {
        Stacks += 1;
    }

    public void TryEffects(float Timer)
    {
        if (TriggerFrequency == 0)
            return;

        this.Timer += Timer;
        if (Timer > 1f / TriggerFrequency)
        {
            this.Timer = 0;
            Effect();
        }
    }

    public bool Compare(StatusAlteration other)
    {
        return string.Equals(Name, other.Name);
    }

    public void Remove(int Stacks)
    {
        if (this.Stacks < Stacks)
            Clear();
        else
            this.Stacks -= Stacks;
    }

    public void Clear()
    {
        OnClear();
        manager.RemoveAlteration(this);
    }

    public virtual bool AllowPlay(Card card) { return true; }
    protected virtual void OnStart(){}
    protected virtual void Effect(){}
    protected virtual void OnClear(){}
}
