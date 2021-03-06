﻿using Assets.Scripts;
using UnityEngine;

public class IncidentSpawner : SingletonBehaviour<IncidentSpawner>
{
    public Spawner incidentSpawner;
    public Spawner asteroidSpawner;

    void OnSpawn(Spawnable spawnable)
    {
#if DEBUG
        var incident = spawnable as Incident;

        if (incident != null)
        {
            Debug.Log($"spawned <{incident.GetType()}>");
        }
#endif
    }

    void Update()
    {
        incidentSpawner.enabled = GameManager.Instance.IsIncidentSpawningEnabled;
        UpdateDebug();
    }

    [System.Diagnostics.Conditional("DEBUG")]
    private void UpdateDebug()
    {
        if (Input.GetKeyUp(KeyCode.F12))
        {
            var spawningEnabled = GameManager.Instance.IsIncidentSpawningEnabled;
            GameManager.Instance.IsIncidentSpawningEnabled = !spawningEnabled;
            Debug.Log($"incident spawning enabled = {!spawningEnabled}");
        }

        if (Input.GetKeyUp(KeyCode.Backspace))
        {
            incidentSpawner.SpawnRandom(true);
        }

        if (Input.GetKeyUp(KeyCode.F1))
        {
            incidentSpawner.Spawn(0, true);
        }

        if (Input.GetKeyUp(KeyCode.F2))
        {
            asteroidSpawner.Spawn(0, true);
        }

        if (Input.GetKeyUp(KeyCode.F3))
        {
            asteroidSpawner.Spawn(1, true);
        }

        if (Input.GetKeyUp(KeyCode.F4))
        {
            asteroidSpawner.Spawn(2, true);
        }

        if (Input.GetKeyUp(KeyCode.F10))
        {
            GameManager.Instance.CurrentOxygen = 5;
        }

        if (Input.GetKeyUp(KeyCode.F11))
        {
            GameManager.Instance.CurrentOxygen = 100;
            GameManager.Instance.IsAsteroidFieldActive = false;
            GameManager.Instance.IsPowerActive = true;
            GameManager.Instance.IsShieldActive = false;
        }
    }
}
