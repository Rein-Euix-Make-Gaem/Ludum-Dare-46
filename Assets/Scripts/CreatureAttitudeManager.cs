using UnityEngine;

public class CreatureAttitudeManager : MonoBehaviour
{
    public Material CreatureMaterial;
    public float CurrentUpsetValue;

    public float BaseCalmingRate;
    public float BaseFreakingRate;
    public float FastCalmingModifier;
    public float FastFreakingModifier;

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

        CreatureMaterial.color = new Color(CurrentUpsetValue / 20f, CreatureMaterial.color.g, CreatureMaterial.color.b);
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
