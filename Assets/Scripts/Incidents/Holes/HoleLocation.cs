using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleLocation : MonoBehaviour
{
    public bool IsActive;
    public SmallHoleInteraction SmallHoleObject;
    // public LargeHoleInteraction LargeHolePrefab;

    // Start is called before the first frame update
    void Start()
    {
        this.IsActive = false;
        this.SmallHoleObject.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (this.SmallHoleObject.gameObject.activeSelf)
        {
            this.IsActive = true;
        }
        else if (!this.SmallHoleObject.gameObject.activeSelf)
        {
            this.IsActive = false;
        }
    }

    public void CreateSmallHole()
    {
        this.IsActive = true;
        this.SmallHoleObject.gameObject.SetActive(true);
        this.SmallHoleObject.Initialize();
    }

    public void CreateLargeHole()
    {
        // TODO
    }
}
