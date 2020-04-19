using Assets.Scripts;
using Assets.Scripts.Interactions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LargePatchPileInteraction : ToggleInteraction
{
    public GameObject target;

    protected override void OnInteract(ref InteractionEvent ev)
    {
        base.OnInteract(ref ev);

        if (this.target != null)
        {
            if (!GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().IsCarryingPatch)
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().PickupLargePatch();
            }
        }
    }
}
