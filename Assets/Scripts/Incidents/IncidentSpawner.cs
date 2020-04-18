using Assets.Scripts.Interactions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncidentSpawner : MonoBehaviour
{
    public List<Incident> Incidents;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Backspace))
        {
            this.SpawnIncident();
        }
    }

    public void SpawnIncident()
    {
        this.Incidents[Random.Range(0, this.Incidents.Count - 1)].InitiateIncident();
    }
}
