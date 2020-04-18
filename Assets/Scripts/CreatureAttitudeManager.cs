using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class CreatureAttitudeManager : MonoBehaviour
{
    public Material CreatureMaterial;
    public float CurrentUpsetValue;

    public float BaseCalmingRate;
    public float BaseFreakingRate;
    public float FastCalmingModifier;
    public float FastFreakingModifier;

    public float rOriginal;
    public float r;
    public float g;
    public float b;


    [SerializeField]
    private bool IsFreakingOut;
    [SerializeField]
    private bool HasDistractions;
    [SerializeField]
    private bool IsPlayerPresent;

    // Start is called before the first frame update
    void Start()
    {
        this.CurrentUpsetValue = 0;
        this.IsFreakingOut = false;
        this.HasDistractions = true;
        this.IsPlayerPresent = false;
        this.rOriginal = this.CreatureMaterial.color.r;
        this.r = this.CreatureMaterial.color.r;
        this.g = this.CreatureMaterial.color.g;
        this.b = this.CreatureMaterial.color.b;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetDistractionsEnabled(bool isEnabled)
    {
        this.HasDistractions = isEnabled;
    }

    // Updates 50 times per second
    private void FixedUpdate()
    {
        if(this.IsPlayerPresent && this.HasDistractions)
        {
            this.CurrentUpsetValue -= this.BaseCalmingRate * this.FastCalmingModifier;
        }
        else if(this.IsPlayerPresent && !this.HasDistractions)
        {
            this.CurrentUpsetValue -= this.BaseCalmingRate;
        }
        else if(!this.IsPlayerPresent && this.HasDistractions)
        {
            this.CurrentUpsetValue += this.BaseFreakingRate;
        }
        else if(!this.IsPlayerPresent && !this.HasDistractions)
        {
            this.CurrentUpsetValue += this.BaseFreakingRate * this.FastFreakingModifier;
        }

        this.CurrentUpsetValue = (this.CurrentUpsetValue < 0) 
            ? 0 
            : (this.CurrentUpsetValue > 100) 
                ? 100 
                : this.CurrentUpsetValue;

        this.CreatureMaterial.SetColor("_Color", new Color(this.CurrentUpsetValue/20f, this.CreatureMaterial.color.g, this.CreatureMaterial.color.b));
        this.r = this.CurrentUpsetValue / 20f;
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            this.IsPlayerPresent = true;
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            this.IsPlayerPresent = false;
        }
    }
}
