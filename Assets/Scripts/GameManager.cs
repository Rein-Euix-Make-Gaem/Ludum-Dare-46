using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : SingletonBehaviour<GameManager>
{
    public Scene TitleScene;
    public Scene MainGame;
    public Scene LoseScene;
    public Scene WinScene;

    public bool SkipTitle;
    public bool NeverLose;

    public bool IsFirstPersonControllerEnabled;

    // Start is called before the first frame update
    void Start()
    {
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
}
