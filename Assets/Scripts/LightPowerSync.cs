using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightPowerSync : MonoBehaviour
{
    public LightController[] lights;

    void Update()
    {
        var powerActive = GameManager.Instance.IsPowerActive;

        for (var i = 0; i < lights.Length; i++)
        {
            lights[i].Toggle(!powerActive);
        }
    }
}
