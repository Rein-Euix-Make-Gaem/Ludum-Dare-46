﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : SingletonBehaviour<GameManager>
{
    public CreatureAttitudeManager creatureAttitudeManager;

    public Scene TitleScene;
    public Scene MainGame;
    public Scene LoseScene;
    public Scene WinScene;

    public bool SkipTitle;
    public bool NeverLose;

    public bool IsIncidentSpawningEnabled;
    public bool IsFirstPersonControllerEnabled;

    public bool IsShieldActive;
    public bool IsPowerActive;
    public bool IsAsteroidSpawningEnabled;
    public bool IsAsteroidFieldActive;

    public Color AlarmColor = Color.white;

    public float CurrentOxygen;
    public float BaseOxygenProductionRate;
    public float BaseSmallOxygenLossRate;
    public float BaseLargeOxygenLossRate;

    public int MajorHoles;
    public int MinorHoles;

    public float SuffocationTime;
    public float ElapsedSuffocationTime;

    private float TotalOxygenReductionRate;
    private bool isProducingOxygen;

    // Start is called before the first frame update

    public bool IsAlarmActive => 
        IsAsteroidFieldActive || CurrentOxygen <= 25;

    void Start()
    {
        IsIncidentSpawningEnabled = true;
        IsAsteroidSpawningEnabled = true;
        IsPowerActive = true;

        this.isProducingOxygen = false;
        this.CurrentOxygen = 100;

        if (this.SkipTitle)
        {
            this.IsFirstPersonControllerEnabled = true;
            SceneManager.LoadScene(this.MainGame.name);
        }
        else
        {
            SceneManager.LoadScene(this.TitleScene.name);
        }
    }

    public void StartGame()
    {
        this.IsFirstPersonControllerEnabled = true;
        SceneManager.LoadScene(this.MainGame.name);
    }

    public void LoseGame()
    {
        SceneManager.MergeScenes(this.LoseScene, this.MainGame);
    }

    public void WinGame()
    {
        SceneManager.MergeScenes(this.WinScene, this.MainGame);
    }

    public void ReturnToTitle()
    {
        SceneManager.LoadScene(this.TitleScene.name);
    }

    public void SetOxygenProduction(bool enableProduction)
    {
        this.isProducingOxygen = enableProduction;

        UpdateDistractions();
    }

    public void AddSmallOxygenLoss()
    {
        this.TotalOxygenReductionRate += this.BaseSmallOxygenLossRate;
        this.MinorHoles++;
    }

    public void AddLargeOxygenLoss()
    {
        this.TotalOxygenReductionRate += this.BaseLargeOxygenLossRate;
        this.MajorHoles++;
    }

    public void RemoveSmallOxygenLoss()
    {
        this.TotalOxygenReductionRate -= this.BaseSmallOxygenLossRate;
        this.MinorHoles--;
    }

    public void RemoveLargeOxygenLoss()
    {
        this.TotalOxygenReductionRate -= this.BaseLargeOxygenLossRate;
        this.MajorHoles--;
    }

    public void SetShieldActive(bool shieldActive)
    {
        Debug.Log($"shields {(shieldActive ? "active" : "disabled")}");

        IsShieldActive = shieldActive;
        UpdateDistractions();
    }

    public void SetPowerActive(bool value)
    {
        Debug.Log($"ship power {(value ? "enabled": "disabled" )}");

        IsPowerActive = value;
        UpdateDistractions();
    }

    public void SetAsteroidFieldActive(bool value)
    {
        IsAsteroidFieldActive = value;
    }

    private void UpdateDistractions()
    {
        // allow distractions if power is active and there are no
        // power-consuming activities

        creatureAttitudeManager.SetDistractionsEnabled(
            IsPowerActive && !IsShieldActive && !isProducingOxygen);
    }

    private void FixedUpdate()
    {
        if (this.isProducingOxygen)
        {
            this.CurrentOxygen += this.BaseOxygenProductionRate;
        }

        this.CurrentOxygen -= this.TotalOxygenReductionRate;

        this.CurrentOxygen = (this.CurrentOxygen < 0)
            ? 0
            : (this.CurrentOxygen > 100)
                ? 100
                : this.CurrentOxygen;

        if(this.CurrentOxygen <= 0 && this.ElapsedSuffocationTime < this.SuffocationTime)
        {
            this.ElapsedSuffocationTime += Time.deltaTime;
        }
        else if(this.CurrentOxygen <= 0 && this.ElapsedSuffocationTime >= this.SuffocationTime && !this.NeverLose)
        {
            this.IsFirstPersonControllerEnabled = false;
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerOxygenObserver>().SuffocateDeath();
        }
        else if(this.CurrentOxygen > 0 && this.ElapsedSuffocationTime > 0)
        {
            this.ElapsedSuffocationTime = 0;
        }
    }
}
