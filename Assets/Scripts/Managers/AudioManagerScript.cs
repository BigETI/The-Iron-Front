using UnityEngine;
using UnityEngine.Audio;

// Audio manager
public class AudioManagerScript : MonoBehaviour
{
    // Audio mixer group
    [SerializeField]
    private AudioMixer audioMixer;

    // Audio mixer matching group names
    [SerializeField]
    private string audioMixerMatchingGroupNames;

    // Repeat music
    [SerializeField]
    private bool repeatMusic = true;

    // Sound effects count
    [SerializeField]
    private uint soundEffectsCount = 8U;

    // Fade in music volume curve
    [SerializeField]
    private AnimationCurve fadeInMusicVolumeCurve;

    // Fade out music volume curve
    [SerializeField]
    private AnimationCurve fadeOutMusicVolumeCurve;

    // Music source
    private AudioSource[] musicSources = null;

    // Fade in times
    private Fade[] fades = null;

    // Sound effect sources
    private AudioSource[] soundEffectSources = null;

    // Current sound effect index
    private uint currentsoundEffectIndex = 0U;

    // Is game paused
    private bool gamePaused = false;

    // Sound effect volume
    private float soundEffectVolume = 1.0f;

    // Music volume
    private float musicVolume = 1.0f;

    // Instance reference
    private static AudioManagerScript instance;

    // Repeat music
    public bool RepeatMusic
    {
        get
        {
            return repeatMusic;
        }
        set
        {
            if (repeatMusic != value)
            {
                repeatMusic = value;
                if (repeatMusic && (musicSources != null))
                {
                    foreach (AudioSource source in musicSources)
                        source.Play();
                }
            }
        }
    }

    // Instance reference
    public static AudioManagerScript Instance
    {
        get
        {
            return instance;
        }
    }

    // Music volume
    public float MusicVolume
    {
        get
        {
            return musicVolume;
        }
        set
        {
            musicVolume = value;
        }
    }

    // Sound efffect volume
    public float SoundEffectVolume
    {
        get
        {
            return soundEffectVolume;
        }
        set
        {
            soundEffectVolume = value;
        }
    }

    // Play music
    public void PlayMusic(AudioClip clip, uint groupID = 0U, float fadeInTime = 0.0f)
    {
        if (fadeInTime < 0.0f)
            fadeInTime = 0.0f;
        if ((clip != null) && (musicSources != null))
        {
            if (groupID < musicSources.Length)
            {
                AudioSource source = musicSources[groupID];
                fades[groupID] = new Fade(EFade.In, fadeInTime);
                if (source.clip != clip)
                {
                    source.clip = clip;
                    source.Play();
                }
            }
        }
    }

    // Stop music
    public void StopMusic(float fadeOutTime = 0.0f)
    {
        repeatMusic = false;
        if (musicSources != null)
        {
            for (int i = 0; i < musicSources.Length; i++)
            {
                fades[i] = new Fade(EFade.Out, fadeOutTime);
                if (fadeOutTime <= 0.0f)
                    musicSources[i].Stop();
            }
        }
    }

    // Stop music
    public void StopMusic(uint groupID, float fadeOutTime = 0.0f)
    {
        repeatMusic = false;
        if (fadeOutTime < 0.0f)
            fadeOutTime = 0.0f;
        if (musicSources != null)
        {
            if (groupID < musicSources.Length)
            {
                fades[groupID] = new Fade(EFade.Out, fadeOutTime);
                if (fadeOutTime <= 0.0f)
                    musicSources[groupID].Stop();
            }
        }
    }

    // Play sound effect
    public void PlaySoundEffect(AudioClip clip)
    {
        if ((clip != null) && (soundEffectSources != null))
        {
            if (soundEffectSources.Length > 0)
            {
                AudioSource audio_source = soundEffectSources[currentsoundEffectIndex];
                audio_source.clip = clip;
                audio_source.volume = soundEffectVolume;
                audio_source.Play();
                ++currentsoundEffectIndex;
                if (currentsoundEffectIndex >= soundEffectSources.Length)
                    currentsoundEffectIndex = 0U;
            }
        }
    }

    // Awake
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            if (audioMixer == null)
            {
                musicSources = new AudioSource[1];
                musicSources[0] = gameObject.AddComponent<AudioSource>();
            }
            else
            {
                AudioMixerGroup[] group = audioMixer.FindMatchingGroups(audioMixerMatchingGroupNames);
                if (group == null)
                {
                    musicSources = new AudioSource[1];
                    musicSources[0] = gameObject.AddComponent<AudioSource>();
                }
                else
                {
                    if (group.Length > 0)
                    {
                        musicSources = new AudioSource[group.Length];
                        for (int i = 0; i < group.Length; i++)
                        {
                            AudioSource source = gameObject.AddComponent<AudioSource>();
                            musicSources[i] = source;
                            source.outputAudioMixerGroup = group[i];
                        }
                    }
                    else
                    {
                        musicSources = new AudioSource[1];
                        musicSources[0] = gameObject.AddComponent<AudioSource>();
                    }
                }
            }
            fades = new Fade[musicSources.Length];
            for (int i = 0; i < fades.Length; i++)
                fades[i] = new Fade();
            soundEffectSources = new AudioSource[soundEffectsCount];
            for (uint i = 0U; i != soundEffectsCount; i++)
                soundEffectSources[i] = gameObject.AddComponent<AudioSource>();
        }
        else
            Destroy(gameObject);
    }

    // Application paused
    private void OnApplicationPause(bool pause)
    {
        gamePaused = pause;
    }

    // Update 
    private void Update()
    {
        if (repeatMusic)
        {
            if (musicSources != null)
            {
                if (!gamePaused)
                {
                    foreach (AudioSource source in musicSources)
                    {
                        if (!source.isPlaying)
                            source.Play();
                    }
                }
            }
        }
        if (musicSources != null)
        {
            for (int i = 0; i < musicSources.Length; i++)
            {
                Fade fade = fades[i];
                switch (fade.fade)
                {
                    case EFade.In:
                        fade.elapsedTime += Time.deltaTime;
                        if (fade.elapsedTime < fade.time)
                            musicSources[i].volume = fadeInMusicVolumeCurve.Evaluate(fade.elapsedTime / fade.time) * musicVolume;
                        else
                        {
                            fade.time = 0.0f;
                            fade.elapsedTime = 0.0f;
                            musicSources[i].volume = musicVolume;
                        }
                        break;
                    case EFade.Out:
                        fade.elapsedTime += Time.deltaTime;
                        if (fade.elapsedTime < fade.time)
                            musicSources[i].volume = fadeOutMusicVolumeCurve.Evaluate(fade.elapsedTime / fade.time) * musicVolume;
                        else
                        {
                            fade.fade = EFade.In;
                            fade.time = 0.0f;
                            fade.elapsedTime = 0.0f;
                            musicSources[i].Stop();
                        }
                        break;
                }
            }
        }
    }
}
