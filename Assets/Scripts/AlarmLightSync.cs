using UnityEngine;

[RequireComponent(typeof(LightController))]
public class AlarmLightSync : MonoBehaviour
{
    public float alarmIntensity = 2f;

    private LightController lightController;
    private Color defaultColor;
    private float defaultIntensity;

    private void Start()
    {
        lightController = GetComponent<LightController>();
        defaultIntensity = lightController.intensity;
        defaultColor = lightController.bulbColor;
    }

    private void Update()
    {
        var alarmActive = GameManager.Instance.IsAlarmActive;
        var alarmColor = alarmActive ? GameManager.Instance.AlarmColor : defaultColor;

        lightController.intensity = alarmActive ? alarmIntensity : defaultIntensity;
        lightController.bulbColor = alarmColor;
    }
}
