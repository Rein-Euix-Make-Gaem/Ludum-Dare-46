using Assets.Scripts;
using Assets.Scripts.Interactions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallHoleInteraction : ToggleInteraction
{
    public GameObject target;
    public ParticleSystem airParticles;

    protected override void OnInteract(ref InteractionEvent ev)
    {
        base.OnInteract(ref ev);

        if (this.target != null)
        {
            this.airParticles.Stop();
            this.target.SetActive(false);
        }
    }

    public void Initialize()
    {
        this.airParticles.Play();
    }
}
