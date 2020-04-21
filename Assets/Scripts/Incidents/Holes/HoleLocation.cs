using Assets.Extensions;
using UnityEngine;

public class HoleLocation : MonoBehaviour
{
    public bool IsActive;
    public SmallHoleInteraction SmallHoleObject;
    public LargeHoleInteraction LargeHoleObject;
    public GameObject CardboardPatchObject;
    public GameObject SmallPatchObject;
    public string hitEvent = "event:/AsteroidHit";

    private FMOD.Studio.EventInstance hitSound;
    private HoleSize LatestHoleSize;

    public AudioSource explosionSound;

    private enum HoleSize
    {
        None,
        Small,
        Large
    }

    void Start()
    {
        this.LatestHoleSize = HoleSize.None;
        this.IsActive = false;
        this.SmallHoleObject.gameObject.SetActive(false);
        this.LargeHoleObject.gameObject.SetActive(false);
        this.CardboardPatchObject.SetActive(false);
        this.SmallPatchObject.SetActive(false);

        hitSound = FMODUnity.RuntimeManager.CreateInstance(hitEvent);
    }

    void Update()
    {

        if (this.SmallHoleObject.gameObject.activeSelf)
        {
            this.IsActive = true;
        }
        else if (this.LargeHoleObject.gameObject.activeSelf)
        {
            this.IsActive = true;
        }
        else if (!this.SmallHoleObject.gameObject.activeSelf && !this.LargeHoleObject.gameObject.activeSelf && this.IsActive)
        {
            this.IsActive = false;

            switch (this.LatestHoleSize)
            {
                case HoleSize.Large: this.CardboardPatchObject.SetActive(true); break;
                case HoleSize.Small: this.SmallPatchObject.SetActive(true); break;
                default: break;
            }
        }
    }

    private void HitSound(Transform transform)
    {
        hitSound.set3DAttributes(transform.ToFModAttributes());
        hitSound.start();

        explosionSound.Play(0);
    }

    public void CreateSmallHole()
    {
        if (!GameManager.Instance.IsShieldActive)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCamera>().SmallShake();

            HitSound(SmallHoleObject.transform);

            this.IsActive = true;

            this.SmallPatchObject.SetActive(false);
            this.CardboardPatchObject.SetActive(false);
            this.LatestHoleSize = HoleSize.Small;

            this.SmallHoleObject.gameObject.SetActive(true);
            this.SmallHoleObject.Initialize();

            GameManager.Instance.AddSmallOxygenLoss();
        }
    }

    public void CreateLargeHole()
    {
        if (!GameManager.Instance.IsShieldActive)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCamera>().BigShake();

            HitSound(LargeHoleObject.transform);

            this.IsActive = true;

            this.SmallPatchObject.SetActive(false);
            this.CardboardPatchObject.SetActive(false);
            this.LatestHoleSize = HoleSize.Large;

            this.LargeHoleObject.gameObject.SetActive(true);
            this.LargeHoleObject.Initialize();

            GameManager.Instance.AddLargeOxygenLoss();
        }
    }
}
