using Doozy.Engine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : SingletonBehaviour<GameManager>
{
    public CreatureAttitudeManager creatureAttitudeManager;

    public string TitleScene;
    public string MainGame;
    public UICanvas LoseScreen;
    public UICanvas WinScreen;

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

    public float TimeToWin;
    public float TimeRemaining;

    private float maxOxygen = 100f;
    private float TotalOxygenReductionRate;
    private bool isProducingOxygen;
    private bool alreadySuffocated;
    private bool isPlaying;
    private float startTime;

    // Start is called before the first frame update

    public bool IsAlarmActive => 
        IsAsteroidFieldActive || CurrentOxygen <= 25;

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        SceneManager.sceneLoaded += this.OnSceneLoaded;

        IsIncidentSpawningEnabled = true;
        IsAsteroidSpawningEnabled = true;
        IsPowerActive = true;

        this.isProducingOxygen = false;
        this.CurrentOxygen = this.maxOxygen;
        this.alreadySuffocated = false;
        this.isPlaying = false;

        if (this.SkipTitle && SceneManager.GetActiveScene().name != this.MainGame)
        {
            this.StartGame();
        }
        else if(SceneManager.GetActiveScene().name != this.TitleScene)
        {
            this.ReturnToTitle();
        }
    }

    public void StartGame()
    {
        this.CurrentOxygen = this.maxOxygen;
        this.alreadySuffocated = false;
        this.IsFirstPersonControllerEnabled = true;
        this.LoseScreen.gameObject.SetActive(false);
        SceneManager.LoadScene(this.MainGame);
    }

    public void LoseGame()
    {
        this.isPlaying = false;
        this.LoseScreen.gameObject.SetActive(true);
    }

    public void WinGame()
    {
        this.isPlaying = false;
        this.WinScreen.gameObject.SetActive(true);
    }

    public void ReturnToTitle()
    {
        this.LoseScreen.gameObject.SetActive(false);
        this.WinScreen.gameObject.SetActive(false);
        this.IsFirstPersonControllerEnabled = false;
        SceneManager.LoadScene(this.TitleScene);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void OnDisable()
    {
        SceneManager.sceneLoaded -= this.OnSceneLoaded;
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        if(scene.name == this.MainGame)
        {
            this.ResetGameState();
            this.creatureAttitudeManager = GameObject.FindGameObjectWithTag("CreatureRoom").GetComponent<CreatureAttitudeManager>();
        }
        else if(scene.name == this.TitleScene)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    private void ResetGameState()
    {
        this.MajorHoles = 0;
        this.MinorHoles = 0;
        this.startTime = Time.fixedTime;
        this.isPlaying = true;
        this.TimeRemaining = this.TimeToWin;
        this.CurrentOxygen = this.maxOxygen;
        this.TotalOxygenReductionRate = 0;
        this.alreadySuffocated = false;
        this.IsFirstPersonControllerEnabled = true;
        this.isProducingOxygen = false;
        this.IsShieldActive = false;
        this.IsPowerActive = true;
        this.IsAsteroidFieldActive = false;
    }

    public void SetOxygenProduction(bool enableProduction)
    {
        this.isProducingOxygen = enableProduction;

        this.UpdateDistractions();
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

        this.IsShieldActive = shieldActive;
        this.UpdateDistractions();
    }

    public void SetPowerActive(bool value)
    {
        Debug.Log($"ship power {(value ? "enabled": "disabled" )}");

        this.IsPowerActive = value;
        this.UpdateDistractions();
    }

    public void SetAsteroidFieldActive(bool value)
    {
        this.IsAsteroidFieldActive = value;
    }

    private void UpdateDistractions()
    {
        // allow distractions if power is active and there are no
        // power-consuming activities

        this.creatureAttitudeManager.SetDistractionsEnabled(
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
        else if(this.CurrentOxygen <= 0 && this.ElapsedSuffocationTime >= this.SuffocationTime && !this.NeverLose && !this.alreadySuffocated)
        {
            this.IsFirstPersonControllerEnabled = false;
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerOxygenObserver>().SuffocateDeath();
            this.alreadySuffocated = true;
        }
        else if(this.CurrentOxygen > 0 && this.ElapsedSuffocationTime > 0)
        {
            this.ElapsedSuffocationTime = 0;
        }

        if (this.isPlaying)
        {
            this.TimeRemaining = this.TimeToWin - (Time.fixedTime - this.startTime);

            if(this.TimeRemaining <= 0)
            {
                this.WinGame();
            }
        }
    }
}
