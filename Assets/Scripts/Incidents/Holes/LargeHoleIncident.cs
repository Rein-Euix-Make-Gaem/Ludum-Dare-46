using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LargeHoleIncident : Incident
{
    public List<HoleLocation> HoleLocations;

    public override void Spawn()
    {
        List<HoleLocation> inactiveLocations = new List<HoleLocation>();
        foreach(HoleLocation loc in this.HoleLocations)
        {
            if (!loc.IsActive)
            {
                inactiveLocations.Add(loc);
            }
        }

        if(inactiveLocations.Count > 0)
        {
            inactiveLocations[Random.Range(0, inactiveLocations.Count)].CreateLargeHole();
        }
    }
}
