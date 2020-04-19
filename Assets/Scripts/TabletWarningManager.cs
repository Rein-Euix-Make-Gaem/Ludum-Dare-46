using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabletWarningManager : MonoBehaviour
{
    public GameObject ReactorWarningObject;
    public GameObject LargeHoleWarningObject;
    public GameObject SmallHoleWarningObject;
    public GameObject AsteroidFieldWarningObject;

    void Start()
    {
        this.ReactorWarningObject.SetActive(false);
        this.LargeHoleWarningObject.SetActive(false);
        this.SmallHoleWarningObject.SetActive(false);
        this.AsteroidFieldWarningObject.SetActive(false);
    }

    void Update()
    {
        var gameManager = GameManager.Instance;

        AsteroidFieldWarningObject.SetActive(gameManager.IsAsteroidFieldActive);
        ReactorWarningObject.SetActive(!gameManager.IsPowerActive);
        LargeHoleWarningObject.SetActive(gameManager.MajorHoles > 0);
        SmallHoleWarningObject.SetActive(gameManager.MinorHoles > 0);
    }
}
