using Assets.Scripts.Interactions;
using System;
using UnityEngine;

public class LightPuzzle : MonoBehaviour
{
    public LightPuzzleInteraction[] items;
    public LightInteraction successLight;
    public string toggleEvent = "event:/LightBlip";
    public string solveEvent = "event:/LightPuzzleWin";

    private int state;
    private FMOD.Studio.EventInstance toggleSound;
    private FMOD.Studio.EventInstance solveSound;

    private const int WIN_STATE = 0b00011111;
    private const int LIGHTS = 5;

    private static readonly int[] solutions = new int[]
    {
        0b01101, 0b10110, 0b11100, 0b10001, 0b00100, 0b01010, 0b00111,
        0b11011, 0b01110, 0b01001, 0b10010, 0b11000, 0b10101, 0b00011
    };

    private void Start()
    {
        state = WIN_STATE;
        Synchronize();
    }
    public void Spawn()
    {
        successLight.Toggle(false);

        Generate();
        Synchronize();

        toggleSound = FMODUnity.RuntimeManager.CreateInstance(toggleEvent);
        solveSound = FMODUnity.RuntimeManager.CreateInstance(solveEvent);
        solveSound.setVolume(0.4f);
    }

    private bool Synchronize()
    {
        var won = IsWon();
        var interactable = !won;

        successLight.Toggle(won);

        for (var i = 0; i < items.Length; i++)
        {
            var value = (state >> i) & 1;
            items[i].puzzleLever.canInteract = interactable;
            items[i].Toggle(value != 0);
        }

        return won;
    }


    private bool IsWon()
    {
        return state == WIN_STATE;
    }

    private void Toggle(int index)
    {
        state ^= (1 << index);

        if (index - 1 >= 0) state ^= (1 << index - 1);
        if (index + 1 < LIGHTS) state ^= (1 << index + 1);

        toggleSound.start();
    }

    private void Generate()
    {
        var index = UnityEngine.Random.Range(0, LIGHTS);
        state = solutions[index];
    }

    public void Interacted(LightPuzzleInteraction interaction)
    {
        var index = Array.FindIndex(items, x => x == interaction);
        
        Toggle(index);

        if (Synchronize())
        {
            solveSound.start();
            GameManager.Instance.SetPowerActive(true);
        }
    }
}
