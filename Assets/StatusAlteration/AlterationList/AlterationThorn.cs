using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlterationThorn : StatusAlteration
{
    GameEventWatcher Watcher;
    private void ActionThorn(GameEvent gameEvent)
    {
        Character Source = manager.Status.Character;
        Character Target = gameEvent.Source;
        CombatManager.Instance.EventManager.Push(new GameEventDealDamage(Source, new List<Character>() { Target }, Stacks));
    }

    protected override void OnStart()
    {
        Character character = manager.Status.Character;
        Watcher = new GameEventWatcher(typeof(GameEventDealDamage), null, character);
        Watcher.SetOnTrigger(ActionThorn);
        CombatManager.Instance.EventManager.AddWatcher(Watcher);
    }

    protected override void OnClear()
    {
        CombatManager.Instance.EventManager.RemoveWatcher(Watcher);
    }
}
