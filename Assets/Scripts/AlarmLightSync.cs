using UnityEngine;

public class AlarmLightSync : MonoBehaviour
{
    public LightController lightController;
    public float alarmIntensity = 10f;

    private Color defaultColor;
    private float defaultIntensity;

    private void Start()
    {
        defaultIntensity = lightController.intensity;
        defaultColor = lightController.bulbColor;
    }

    private void Update()
    {
        var alarmActive = GameManager.Instance.IsAlarmActive;
        var alarmColor = alarmActive ? GameManager.Instance.AlarmColor : defaultColor;

        lightController.intensity = alarmIntensity;
        lightController.bulbColor = alarmColor;
    }
}
