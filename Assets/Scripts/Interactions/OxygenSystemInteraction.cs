using Assets.Scripts;
using Assets.Scripts.Interactions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OxygenSystemInteraction : ToggleInteraction
{
    public GameObject target;

    private bool oxygenEnabled = false;

    public string activatedEvent = "";
    FMOD.Studio.EventInstance activatedSound;


    private Awake(){
        activatedSound = FMODUnity.RuntimeManager.CreateInstance(activatedEvent);
    }

    protected override void OnInteract(ref InteractionEvent ev)
    {
        base.OnInteract(ref ev);

        this.oxygenEnabled = !this.oxygenEnabled;

        if (this.oxygenEnabled){        
            activatedSound.start();
        }
        else {
            //FMODUnity.RuntimeManager.PlayOneShot("event:/OxygenDeactivated");
        }

        GameManager.Instance.SetOxygenProduction(this.oxygenEnabled);

        // If oxygen is enabled, disable distractions.
        GameObject.FindGameObjectWithTag("CreatureRoom").GetComponent<CreatureAttitudeManager>().SetDistractionsEnabled(!this.oxygenEnabled);
    
    }
}
