using UnityEngine;

// Moody music controller
public class MoodyMusicControllerScript : MonoBehaviour
{
    // Morning music
    [SerializeField]
    private AudioClip morningMusicClip;

    // Noon music
    [SerializeField]
    private AudioClip noonMusicClip;

    // Evening music
    [SerializeField]
    private AudioClip eveningMusicClip;

    // Night music
    [SerializeField]
    private AudioClip nightMusicClip;

    // Last day time
    private EDayTime lastDayTime = EDayTime.Noon;

    // Update music
    private void UpdateMusic()
    {
        if (AudioManagerScript.Instance != null)
        {
            switch (lastDayTime)
            {
                case EDayTime.Morning:
                    if (morningMusicClip != null)
                    {
                        AudioManagerScript.Instance.StopMusic(2.0f);
                        AudioManagerScript.Instance.PlayMusic(morningMusicClip, 0U, 2.0f);
                    }
                    break;
                case EDayTime.Noon:
                    if (noonMusicClip != null)
                    {
                        AudioManagerScript.Instance.StopMusic(2.0f);
                        AudioManagerScript.Instance.PlayMusic(noonMusicClip, 1U, 2.0f);
                    }
                    break;
                case EDayTime.Evening:
                    if (eveningMusicClip != null)
                    {
                        AudioManagerScript.Instance.StopMusic(2.0f);
                        AudioManagerScript.Instance.PlayMusic(eveningMusicClip, 2U, 2.0f);
                    }
                    break;
                case EDayTime.Night:
                    if (nightMusicClip != null)
                    {
                        AudioManagerScript.Instance.StopMusic(2.0f);
                        AudioManagerScript.Instance.PlayMusic(nightMusicClip, 3U, 2.0f);
                    }
                    break;
            }
        }
    }

    // Start
    private void Start()
    {
        lastDayTime = TimeManagerScript.Instance.DayTime;
        UpdateMusic();
    }

    // Update is called once per frame
    private void Update()
    {
        if (TimeManagerScript.Instance != null)
        {
            EDayTime dt = TimeManagerScript.Instance.DayTime;
            if (dt != lastDayTime)
            {
                lastDayTime = dt;
                UpdateMusic();
            }
        }
    }
}
