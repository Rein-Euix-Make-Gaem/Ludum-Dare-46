using UnityEngine;

public class LightController : MonoBehaviour
{
    public GameObject bulb;
    public Color bulbColor = Color.white;
    public float intensity = 1f;
    public float lightIntensityMultiplier = 10f;
    public float responseTime = 25;
    public bool on = true;

    private Light attachedLight;
    private MeshRenderer bulbRenderer;
    private Material bulbMaterial;
    private float currentIntensity = 0f;

    public void Start()
    {
        bulbRenderer = bulb.GetComponent<MeshRenderer>();
        bulbMaterial = new Material(bulbRenderer.sharedMaterial);
        bulbMaterial.color = bulbColor;
        bulbRenderer.material = bulbMaterial;
        attachedLight = GetComponentInChildren<Light>();
    }

    public void Update()
    {
        var targetIntensity = on ? intensity : 0f;

        currentIntensity = Mathf.Lerp(currentIntensity, targetIntensity, Time.deltaTime * responseTime);
        bulbMaterial.color = bulbColor;
        bulbMaterial.SetColor("_EmissionColor", bulbColor * currentIntensity);

        if (attachedLight != null)
        {
            attachedLight.color = bulbColor;
            attachedLight.intensity = currentIntensity * lightIntensityMultiplier;
        }
    }

    public void Toggle(bool value)
    {
        on = value;
    }
}
