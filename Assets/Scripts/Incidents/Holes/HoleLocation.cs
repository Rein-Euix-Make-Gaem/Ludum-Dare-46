using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleLocation : MonoBehaviour
{
    public bool IsActive;
    public SmallHoleInteraction SmallHoleObject;
    public LargeHoleInteraction LargeHoleObject;
    public GameObject CardboardPatchObject;
    public GameObject SmallPatchObject;

    private HoleSize LatestHoleSize;
    private enum HoleSize
    {
        None,
        Small,
        Large
    }

    // Start is called before the first frame update
    void Start()
    {
        this.LatestHoleSize = HoleSize.None;
        this.IsActive = false;
        this.SmallHoleObject.gameObject.SetActive(false);
        this.LargeHoleObject.gameObject.SetActive(false);
        this.CardboardPatchObject.SetActive(false);
        this.SmallPatchObject.SetActive(false);
    }

    // Update is called once per frame
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

    public void CreateSmallHole()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCamera>().SmallShake();

        this.IsActive = true;

        this.SmallPatchObject.SetActive(false);
        this.CardboardPatchObject.SetActive(false);
        this.LatestHoleSize = HoleSize.Small;

        this.SmallHoleObject.gameObject.SetActive(true);
        this.SmallHoleObject.Initialize();
    }

    public void CreateLargeHole()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCamera>().BigShake();

        this.IsActive = true;

        this.SmallPatchObject.SetActive(false);
        this.CardboardPatchObject.SetActive(false);
        this.LatestHoleSize = HoleSize.Large;

        this.LargeHoleObject.gameObject.SetActive(true);
        this.LargeHoleObject.Initialize();
    }
}
