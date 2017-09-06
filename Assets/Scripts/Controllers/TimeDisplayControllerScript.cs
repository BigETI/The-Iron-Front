using UnityEngine;
using UnityEngine.UI;

// Time display controller
public class TimeDisplayControllerScript : MonoBehaviour
{

    // Time display text
    [SerializeField]
    private Text timeDisplayText;

    // Update
    private void Update()
    {
        if ((timeDisplayText != null) && (TimeManagerScript.Instance != null))
            timeDisplayText.text = TimeManagerScript.Instance.TimeText;
    }
}
