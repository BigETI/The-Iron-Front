using UnityEngine;

public class AudioTriggerScript : MonoBehaviour
{

    [SerializeField]
    private AudioClip clip;

    [SerializeField]
    private float volume;

    [SerializeField]
    private bool repeatMusic = true;

    [SerializeField]
    private EAudioType audioType;

    private void Start()
    {
        if (AudioManagerScript.Instance != null)
        {
            switch (audioType)
            {
                case EAudioType.SoundEffect:
                    AudioManagerScript.Instance.SoundEffectVolume = volume;
                    AudioManagerScript.Instance.PlaySoundEffect(clip);
                    break;
                case EAudioType.Music:
                    AudioManagerScript.Instance.MusicVolume = volume;
                    AudioManagerScript.Instance.RepeatMusic = repeatMusic;
                    AudioManagerScript.Instance.StopMusic(2.0f);
                    AudioManagerScript.Instance.PlayMusic(clip);
                    break;
            }
        }
        Destroy(this);
    }
}
