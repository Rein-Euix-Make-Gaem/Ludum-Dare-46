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
        int secs = Mathf.RoundToInt(gameManager.TimeRemaining % 60);

        string seconds = secs < 10 ? "0" + secs.ToString() : secs.ToString();

        this.WinTimer.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
