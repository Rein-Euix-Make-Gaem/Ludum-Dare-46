﻿using Assets.Scripts.Interactions;
using System;
using UnityEngine;

public class LightPuzzle : MonoBehaviour
{
    public LightPuzzleInteraction[] items;
    public LightInteraction successLight;
    public string toggleEvent = "event:/LightBlip";
    public string solveEvent = "event:/LightPuzzleWin";

    private bool[] state;
    private FMOD.Studio.EventInstance toggleSound;
    private FMOD.Studio.EventInstance solveSound;

    public void Spawn()
    {
        successLight.Toggle(false);

        Generate(state);
        Synchronize();

        toggleSound = FMODUnity.RuntimeManager.CreateInstance(toggleEvent);
        solveSound = FMODUnity.RuntimeManager.CreateInstance(solveEvent);
        solveSound.setVolume(0.4f);
    }

    private void Start()
    {
        state = new bool[items.Length];

        for (var i = 0; i < state.Length; i++)
        {
            state[i] = true;
        }

        Synchronize();
    }

    private bool Synchronize()
    {
        var won = IsWon(state);
        var interactable = !won;

        successLight.Toggle(won);

        for (var i = 0; i < items.Length; i++)
        {
            items[i].puzzleLever.canInteract = interactable;
            items[i].Toggle(state[i]);
        }

        return won;
    }


    private bool IsWon(bool[] state)
    {
        for (var i = 0; i < state.Length; i++)
        {
            if (!state[i]) return false;
        }

        return true;
    }

    private bool Validate(bool[] state)
    {
        var copy = new bool[state.Length];
        var iterations = 50;

        for (var i = 0; i < iterations; i++)
        {
            // reset the game state
            Array.Copy(state, 0, copy, 0, state.Length);

            // toggle random bits
            for (var j = 0; j < copy.Length; j++)
            {
                var n = UnityEngine.Random.Range(0, copy.Length);
                Toggle(copy, n);
            }

            // check for win condition
            if (IsWon(copy))
            {
                return true;
            }
        }

        // could not validate puzzle
        return false;
    }

    private void Toggle(bool[] state, int index)
    {
        state[index] = !state[index];
        
        if (index - 1 >= 0) state[index - 1] = !state[index - 1];
        if (index + 1 < state.Length) state[index + 1] = !state[index + 1];

        toggleSound.start();

    }

    private void Reset()
    {
        for (var i = 0; i < state.Length; i++)
        {
            state[i] = false;
        }
    }

    private void Generate(bool[] state)
    {
        var count = 0;

        Reset();

        while (!Validate(state))
        {
            // toggle random bits
            for (var i = 0; i < state.Length; i++)
            {
                state[i] = UnityEngine.Random.value >= 0.5f;
                count += state[i] ? 1 : 0;
            }

            // do not generate empty or full states
            if (count == 0 || count == 5)
                continue;
        }
    }

    public void Interacted(LightPuzzleInteraction interaction)
    {
        var index = Array.FindIndex(items, x => x == interaction);
        
        Toggle(state, index);

        if (Synchronize())
        {
            solveSound.start();
            GameManager.Instance.SetPowerActive(true);
        }
    }
}
