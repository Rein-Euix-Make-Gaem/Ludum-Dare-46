using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmLights : MonoBehaviour
{
    public float pulsesPerSecond = 0.5f;
    public Color color = Color.red;

    void Update()
    {
        var from = Color.red * 0.1f;
        var alarmColor = Color.Lerp(from, color, Mathf.PingPong(Time.time * pulsesPerSecond, 1f));

        if (GameManager.Instance.IsAlarmActive) 
        {
            GameManager.Instance.AlarmColor = alarmColor;
        }
        else
        {
            GameManager.Instance.AlarmColor = Color.black;
        }
    }
}
