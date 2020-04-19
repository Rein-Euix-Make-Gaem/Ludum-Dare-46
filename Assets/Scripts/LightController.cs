using UnityEngine;

public class LightController : MonoBehaviour
{
    public GameObject bulb;
    public Color bulbColor = Color.white;
    public float intensity = 1f;
    public float responseTime = 25;

    private MeshRenderer bulbRenderer;
    private Material bulbMaterial;
    private float currentIntensity = 0f;

    public void Start()
    {
        bulbRenderer = bulb.GetComponent<MeshRenderer>();
        bulbMaterial = bulbRenderer.material;
        bulbMaterial.color = bulbColor;
        bulbRenderer.material = bulbMaterial;
    }

    public void Update()
    {
        currentIntensity = Mathf.Lerp(currentIntensity, intensity, Time.deltaTime * responseTime);
        bulbMaterial.color = bulbColor;
        bulbMaterial.SetColor("_EmissionColor", bulbColor * currentIntensity);
    }
}
