﻿using Assets.Scripts;
using Assets.Scripts.Interactions;
using TMPro;
using UnityEngine;

public class OxygenSystemInteraction : ToggleInteraction
{
    public TMP_Text ActiveStatusText;
    public GameObject ScreenObject;

    private bool oxygenEnabled = false;


    public string activatedEvent = "event:/OxygenActivated";
    FMOD.Studio.EventInstance activatedSound;

    public string deActivationEvent = "event:/OxygenDeactivated";
    FMOD.Studio.EventInstance deActivationSound;

    private bool isCurrentlyPowered = true;
    private string inactive = "INACTIVE";
    private string active = "ACTIVE";

    private void Update()
    {
        if (GameManager.Instance.IsPowerActive != this.isCurrentlyPowered)
        {
            this.isCurrentlyPowered = GameManager.Instance.IsPowerActive;
            this.ScreenObject.SetActive(this.isCurrentlyPowered);
            this.canInteract = this.isCurrentlyPowered;
        }
    }

    protected override void OnStart()
    {
        base.OnStart();

        this.ActiveStatusText.text = this.inactive;
        activatedSound = FMODUnity.RuntimeManager.CreateInstance(activatedEvent);
        deActivationSound = FMODUnity.RuntimeManager.CreateInstance(deActivationEvent);
    }

    protected override void OnInteract(ref InteractionEvent ev)
    {
        base.OnInteract(ref ev);

        this.oxygenEnabled = !this.oxygenEnabled;

        if (this.oxygenEnabled)
        {
            deActivationSound.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            activatedSound.start();
        }
        else {
            activatedSound.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            deActivationSound.start();
        }

        this.ActiveStatusText.text = this.oxygenEnabled ? this.active : this.inactive;


        GameManager.Instance.SetOxygenProduction(this.oxygenEnabled);

        // If oxygen is enabled, disable distractions.
        GameObject.FindGameObjectWithTag("CreatureRoom").GetComponent<CreatureAttitudeManager>().SetDistractionsEnabled(!this.oxygenEnabled);
    
    }
}
