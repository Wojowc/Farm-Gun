using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HeroWindowTrigger : MonoBehaviour
{
    public string title;
    public Sprite sprite;
    public string message;
    public bool triggerOnEnable;

    public void OnEnable()
    {
        if (!triggerOnEnable) { return; }

        if(UIController.instance == null)
        {
            Debug.Log("UIController is null wtf");
            return;
        }

        UIController.instance.ModalWindow.ShowAsHero(title, sprite, message, null, DoAlternateStuff, null);
    }

    private void DoAlternateStuff()
    {
        Debug.Log("Alternative");
    }
}
