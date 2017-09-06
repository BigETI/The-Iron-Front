using UnityEngine;

// Light source controller
public class LightSourceControllerScript : MonoBehaviour
{
    // Enable at day time
    [SerializeField]
    private EDayTime[] enableAtDayTime;

    // Light intensity
    [SerializeField]
    private float lightIntensity = 1.0f;

    // Is enabled
    private bool isEnabled = false;

    // Light source light
    private Light lightSourceLight;

    // Update light
    private void UpdateLight()
    {
        if ((lightSourceLight != null) && (TimeManagerScript.Instance != null))
        {
            isEnabled = false;
            foreach (EDayTime dt in enableAtDayTime)
            {
                isEnabled = (TimeManagerScript.Instance.DayTime == dt);
                if (isEnabled)
                    break;
            }
            //lightSourceLight.intensity = isEnabled ? lightIntensity : 0.0f;
            lightSourceLight.intensity = lightIntensity;
            lightSourceLight.enabled = isEnabled;
        }
    }

    // Start
    private void Start()
    {
        lightSourceLight = GetComponent<Light>();
        UpdateLight();
    }

    // Update
    private void Update()
    {
        UpdateLight();
    }
}
