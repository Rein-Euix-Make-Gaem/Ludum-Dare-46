using Assets.Scripts;
using Assets.Scripts.Interactions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OxygenSystemInteraction : ToggleInteraction
{
    public GameObject target;

    private bool oxygenEnabled = false;

    protected override void OnInteract(ref InteractionEvent ev)
    {
        base.OnInteract(ref ev);

        this.oxygenEnabled = !this.oxygenEnabled;

        GameManager.Instance.SetOxygenProduction(this.oxygenEnabled);

        // If oxygen is enabled, disable distractions.
        GameObject.FindGameObjectWithTag("CreatureRoom").GetComponent<CreatureAttitudeManager>().SetDistractionsEnabled(!this.oxygenEnabled);
    
        if (this.oxygenEnabled){
            FMODUnity.RuntimeManager.PlayOneShot("event:/OxygenActivated");
        }
        else {
            FMODUnity.RuntimeManager.PlayOneShot("event:/OxygenDeactivated");
        }
    
    
    
    
    }
}
