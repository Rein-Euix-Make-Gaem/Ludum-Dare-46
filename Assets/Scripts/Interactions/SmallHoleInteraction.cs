using Assets.Extensions;
using Assets.Scripts;
using Assets.Scripts.Interactions;
using UnityEngine;

public class SmallHoleInteraction : ToggleInteraction
{
    public GameObject target;
    public ParticleSystem airParticles;
    public string patchEvent = "event:/Patch Hole";

    private FMOD.Studio.EventInstance patchSound;

    protected override void OnStart() 
    {
        patchSound = FMODUnity.RuntimeManager.CreateInstance(patchEvent);
    }

    protected override void OnInteract(ref InteractionEvent ev)
    {
        base.OnInteract(ref ev);

        if (this.target != null)
        {
            patchSound.set3DAttributes(target.transform.ToFModAttributes());
            patchSound.start();

            this.airParticles.Stop();
            GameManager.Instance.RemoveSmallOxygenLoss();
            this.target.SetActive(false);
        }
    }

    public void Initialize()
    {
        this.airParticles.Play();
    }
}
