using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TabletWarningManager : MonoBehaviour
{
    public GameObject ReactorWarningObject;
    public GameObject LargeHoleWarningObject;
    public GameObject SmallHoleWarningObject;
    public GameObject AsteroidFieldWarningObject;
    public TMP_Text WinTimer;

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

        string minutes = ((int)gameManager.TimeRemaining / 60).ToString();
        string seconds = (gameManager.TimeRemaining % 60).ToString("f0");

        this.WinTimer.text = minutes + ":" + seconds;
    }
}
