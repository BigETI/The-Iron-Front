using UnityEngine;

// Day night cycle manager
[RequireComponent(typeof(Light))]
public class DayNightCycleControllerScript : MonoBehaviour
{
    // Sun angle
    [SerializeField]
    private Vector3 sunAngle;

    // Sun light
    private Light sunLight;

    // Start
    private void Start()
    {
        sunLight = GetComponent<Light>();
    }

    // Update
    private void Update()
    {
        if ((sunLight != null) && (TimeManagerScript.Instance != null))
        {
            float t = TimeManagerScript.Instance.ElapsedDayTime;
            transform.rotation = Quaternion.AngleAxis(t * 360.0f, sunAngle);
            sunLight.intensity = ((t >= 0.0f) && (t < 0.5f)) ? 1.0f : 0.0f;
        }
    }
}
