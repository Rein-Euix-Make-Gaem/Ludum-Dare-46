using Assets.Scripts;
using Assets.Scripts.Interactions;
using UnityEngine;

public class LargePatchPileInteraction : Interactable
{
    public GameObject target;

    private PlayerController player =>
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

    public override string description => player.IsCarryingPatch
        ? "drop patch" : "pickup patch";

    protected override void OnInteract(ref InteractionEvent ev)
    {
        base.OnInteract(ref ev);

        if (target != null)
        { 
            if (!player.IsCarryingPatch)
            {
                player.PickupLargePatch();
            }
            else
            {
                player.DropLargePatch();
            }
        }
    }
}
