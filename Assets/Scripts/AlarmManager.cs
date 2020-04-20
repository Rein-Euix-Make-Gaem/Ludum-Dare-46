using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmManager : MonoBehaviour
{
    public AudioSource alarmSound;
    public float pulsesPerSecond = 0.7f;
    public bool syncToAudio = true;
    public Color color = Color.red;

    private float time;

    private void Start()
    {
        if (syncToAudio)
        {
            pulsesPerSecond = 1f / alarmSound.clip.length;
        }
    }

    void Update()
    {
        if (GameManager.Instance.IsAlarmActive) 
        {
            if (!alarmSound.isPlaying)
            {
                alarmSound.Play(0);
                time = 0;
            }

            time += Time.deltaTime;

            var frequency = pulsesPerSecond;
            var angle = 2 * Mathf.PI;
            var alpha = (Mathf.Sin(frequency * angle * time) + 1) * 0.5f;

            var from = Color.red * 0.1f;
            var alarmColor = Color.Lerp(from, color, alpha);

            GameManager.Instance.AlarmColor = alarmColor;
        }
        else
        {
            if (alarmSound.isPlaying)
            {
                alarmSound.Stop();
            }

            GameManager.Instance.AlarmColor = Color.black;
        }
    }
}
