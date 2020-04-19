using Assets.Scripts;
using Assets.Scripts.Interactions;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OxygenSystemInteraction : ToggleInteraction
{
    public GameObject target;
    public TMP_Text ActiveStatusText;

    private bool oxygenEnabled = false;

    private string inactive = "INACTIVE";
    private string active = "ACTIVE";

    private void Start()
    {
        this.ActiveStatusText.text = this.inactive;
    }

    protected override void OnInteract(ref InteractionEvent ev)
    {
        base.OnInteract(ref ev);

        this.oxygenEnabled = !this.oxygenEnabled;

        this.ActiveStatusText.text = this.oxygenEnabled ? this.active : this.inactive;

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
