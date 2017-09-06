// Fade audio data
public class Fade
{
    // Fade
    public EFade fade = EFade.In;

    // Time
    public float time = 0.0f;

    // Elapsed time
    public float elapsedTime = 0.0f;

    // Default constructor
    public Fade()
    {
        //
    }

    // Constructor
    public Fade(EFade fade, float time)
    {
        this.fade = fade;
        this.time = time;
    }
}
