using UnityEngine;

public class LightController : MonoBehaviour
{
    public GameObject bulb;
    public Color bulbColor = Color.white;
    public float intensity = 1f;
    public float lightIntensityMultiplier = 10f;
    public float responseTime = 25;
    public bool inheritIntesity = true;
    public bool on = true;

    private Light attachedLight;
    private MeshRenderer bulbRenderer;
    private Material bulbMaterial;
    private Color currentColor;
    private float currentIntensity = 0f;

    public void Start()
    {
        if (bulb != null)
        {
            bulbRenderer = bulb.GetComponent<MeshRenderer>();
            bulbMaterial = new Material(bulbRenderer.sharedMaterial);
            bulbMaterial.color = bulbColor;
            bulbRenderer.material = bulbMaterial;
        }

        attachedLight = GetComponent<Light>() ?? GetComponentInChildren<Light>();

        if (attachedLight != null && inheritIntesity)
        {
            intensity = attachedLight.intensity;
        }
    }

    public void Update()
    {
        var targetIntensity = on ? intensity : 0f;

        currentColor = bulbColor;
        currentIntensity = Mathf.Lerp(currentIntensity, targetIntensity, Time.deltaTime * responseTime);

        if (bulbMaterial != null)
        {
            bulbMaterial.color = currentColor;
            bulbMaterial.SetColor("_EmissionColor", currentColor * currentIntensity);
        }

        if (attachedLight != null)
        {
            attachedLight.color = currentColor;
            attachedLight.intensity = currentIntensity * lightIntensityMultiplier;
        }
    }

    public void Toggle(bool value)
    {
        on = value;
    }
}
