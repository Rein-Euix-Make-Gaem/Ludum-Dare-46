using Assets.Extensions;
using Assets.Scripts;
using Assets.Scripts.Interactions;
using UnityEngine;

public class LargeHoleInteraction : ToggleInteraction
{
    public GameObject target;
    public ParticleSystem airParticles;
    public string patchEvent = "event:/Patch Hole";
    public AudioSource windSound;

    private PlayerController playerController;
    private FMOD.Studio.EventInstance patchSound;

    protected override void OnInteract(ref InteractionEvent ev)
    {
        base.OnInteract(ref ev);

        if (this.target != null)
        {
            this.windSound.Stop();
            patchSound.set3DAttributes(target.transform.ToFModAttributes());
            patchSound.start();

            this.playerController.DropLargePatch();
            this.airParticles.Stop();
            GameManager.Instance.RemoveLargeOxygenLoss();
            this.target.SetActive(false);
        }
    }

    public override bool CanInteract(PlayerController player)
    {
        this.playerController = player;
        return player.IsCarryingPatch;
    }

    public void Initialize()
    {
        this.airParticles.Play();
        this.windSound.Play(0);
        patchSound = FMODUnity.RuntimeManager.CreateInstance(patchEvent);
    }
}
