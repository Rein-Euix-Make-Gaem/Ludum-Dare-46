using Assets.Scripts;
using Assets.Scripts.Interactions;
using System;
using UnityEngine;

public class LightSwitchInteraction : ToggleInteraction
{
    public Animator animator;
    public LightController[] lights;

    protected override void OnInteract(ref InteractionEvent ev)
    {
        base.OnInteract(ref ev);

        animator.SetTrigger("Toggle");

        for (var i = 0; i < lights.Length; i++)
        {
            lights[i].Toggle(!lights[i].on);
        }
    }
}
