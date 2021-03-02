using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class ButtonEndTargetting : MonoBehaviour
{
    Button Button;
    Text Text;
    GameEventManager EventManager;


    void Start()
    {
        EventManager = CombatManager.Instance.EventManager;
        Button = GetComponent<Button>();
        Text = GetComponentInChildren<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        Button.interactable = IsEnabled();
        UpdateText();
    }

    bool IsEnabled()
    {
        if (EventManager.Events.Count == 0)
            return false;

        Type ActiveEvent = EventManager.GetActiveEvent();

        if (ActiveEvent == null)
            return false;


        if (ActiveEvent.IsSubclassOf(typeof(Targetter<Character>)))
            return true;

        if (ActiveEvent.IsSubclassOf(typeof(Targetter<Card>)))
            return true;

        return false;
    }

    void UpdateText()
    {
        if (!Button.interactable)
            Text.text = "";
        else //if (((Targetter<Targetable>) EventManager.Events.Peek()).TargetList.Count == 0)
            Text.text = "Done";
    }
}
