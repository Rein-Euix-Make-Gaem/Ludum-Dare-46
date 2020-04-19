using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmLights : MonoBehaviour
{
    public float pulsesPerSecond = 1.5f;
    public Color color = Color.red;

    private Light[] directionalLights;
    private Color[] baseColors;
    private float time;

    void Start()
    {
        directionalLights = GetComponentsInChildren<Light>();
        baseColors = new Color[directionalLights.Length];

        for (var i = 0; i < directionalLights.Length; i++)
        {
            baseColors[i] = directionalLights[i].color;
        }
    }

    bool IsAlarmActive
    {
        get
        {
            var manager = GameManager.Instance;
            return manager.IsAsteroidFieldActive || manager.CurrentOxygen <= 25;
        }
    }

    void Update()
    {
        if (GameManager.Instance.IsAlarmActive)
        {
            time += Time.deltaTime;

            for (var i = 0; i < directionalLights.Length; i++)
            {
                var from = Color.red * 0.2f;
                var alarmColor = Color.Lerp(from, color, Mathf.PingPong(time * pulsesPerSecond, 1f));

                directionalLights[i].color = alarmColor;

                GameManager.Instance.AlarmColor = alarmColor;
            }
        }
        else
        {
            time = 0;

            for (var i = 0; i < directionalLights.Length; i++)
            {
                directionalLights[i].color = baseColors[i];
            }
        }
    }
}
