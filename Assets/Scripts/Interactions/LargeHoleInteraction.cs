using Assets.Scripts;
using Assets.Scripts.Interactions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LargeHoleInteraction : ToggleInteraction
{
    public GameObject target;
    public ParticleSystem airParticles;

    private PlayerController playerController;

    public string patchEvent = "event:/Patch Hole";
    FMOD.Studio.EventInstance patchSound;

    protected override void OnInteract(ref InteractionEvent ev)
    {
        base.OnInteract(ref ev);

        if (this.target != null)
        {
            this.playerController.DropLargePatch();
            this.airParticles.Stop();
            GameManager.Instance.RemoveLargeOxygenLoss();
            this.target.SetActive(false);
        }

        patchSound.start();

    }

    public override bool CanInteract(PlayerController player)
    {
        this.playerController = player;
        return player.IsCarryingPatch;
    }

    public void Initialize()
    {
        this.airParticles.Play();
        patchSound = FMODUnity.RuntimeManager.CreateInstance(patchEvent);
    }
}
