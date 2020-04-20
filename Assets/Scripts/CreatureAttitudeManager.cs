using System.Collections;
using UnityEngine;

public class CreatureAttitudeManager : MonoBehaviour
{
    public GameObject CreatureObject;
    public Material CreatureMaterial;
    public ParticleSystem CreatureExplosion;
    public GameObject TVScreen;

    public float CurrentUpsetValue;

    public float BaseCalmingRate;
    public float BaseFreakingRate;
    public float FastCalmingModifier;
    public float FastFreakingModifier;

    [SerializeField]
    private bool HasDistractions;
    [SerializeField]
    private bool IsPlayerPresent;
    private Material dynamicCreatureMaterial;

    public string worriedEvent = "event:/Worried";
    FMOD.Studio.EventInstance worriedSound;

    // Start is called before the first frame update
    void Start()
    {
        this.CurrentUpsetValue = 0;
        this.HasDistractions = true;
        this.IsPlayerPresent = false;

        worriedSound = FMODUnity.RuntimeManager.CreateInstance(worriedEvent);
        dynamicCreatureMaterial = new Material(CreatureMaterial);
    }

    // Update is called once per frame
    void Update()
    {
        if(this.CurrentUpsetValue >= 100 
            && !GameManager.Instance.NeverLose
            && GameManager.Instance.IsFirstPersonControllerEnabled)
        {
            GameManager.Instance.IsFirstPersonControllerEnabled = false;
            this.StartLose();
        }
    }

    public void SetDistractionsEnabled(bool isEnabled)
    {
        this.HasDistractions = isEnabled;
        this.TVScreen.SetActive(this.HasDistractions);
    }

    private void StartLose()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCamera>().BigShake();
        this.CreatureExplosion.Play();
        this.CreatureObject.SetActive(false);
        FMODUnity.RuntimeManager.PlayOneShot("event:/Death");

        StartCoroutine(WaitForDeath());
    }

    IEnumerator WaitForDeath()
    {
        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(2);

        GameManager.Instance.LoseGame();
    }

    // Updates 50 times per second
    private void FixedUpdate()
    {
        float start = this.CurrentUpsetValue;

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

        CurrentUpsetValue = Mathf.Clamp(CurrentUpsetValue, 0, 100);

        if (start <= 66 && this.CurrentUpsetValue > 66){
            worriedSound.start();
        }

        dynamicCreatureMaterial.color = new Color(CurrentUpsetValue / 20f, dynamicCreatureMaterial.color.g, dynamicCreatureMaterial.color.b);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/PlayerEntersRoom"); 
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
