using Assets.Scripts;
using Assets.Scripts.Interactions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallHoleInteraction : ToggleInteraction
{
    public GameObject target;
    public ParticleSystem airParticles;

    public string patchEvent = "event:/Patch Hole";
    FMOD.Studio.EventInstance patchSound;

    public void Start(){
        patchSound = FMODUnity.RuntimeManager.CreateInstance(patchEvent);
    }


    protected override void OnInteract(ref InteractionEvent ev)
    {
        base.OnInteract(ref ev);

        if (this.target != null)
        {
            this.airParticles.Stop();
            GameManager.Instance.RemoveSmallOxygenLoss();
            this.target.SetActive(false);
        }

        patchSound.start();

    }

    public void Initialize()
    {
        this.airParticles.Play();
    }
}
