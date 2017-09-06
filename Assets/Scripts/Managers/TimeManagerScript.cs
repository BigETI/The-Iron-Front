using UnityEngine;

// Time manager
public class TimeManagerScript : MonoBehaviour
{
    // Time time
    [Range(1.0f, 10000.0f)]
    [SerializeField]
    private float dayTimeTime = 600.0f;

    // Start time
    [SerializeField]
    private float startTime = 0.0f;

    // Elapsed time time
    private float elapsedDayTimeTime = 0.0f;

    // Instance reference
    private static TimeManagerScript instance = null;

    // Instance reference
    public static TimeManagerScript Instance
    {
        get
        {
            return instance;
        }
    }

    // Time
    public float ElapsedDayTime
    {
        get
        {
            return (elapsedDayTimeTime / dayTimeTime);
        }
    }

    // Day time
    public EDayTime DayTime
    {
        get
        {
            EDayTime ret = EDayTime.Morning;
            float t = ElapsedDayTime;
            if ((t >= 0.25f) && (t < 0.5f))
                ret = EDayTime.Noon;
            else if ((t >= 0.5f) && (t < 0.75f))
                ret = EDayTime.Evening;
            else if ((t >= 0.75f) && (t < 1.0f))
                ret = EDayTime.Night;
            return ret;
        }
    }

    // Time text
    public string TimeText
    {
        get
        {
            float t = ElapsedDayTime + 0.25f;
            if (t >= 1.0f)
                t = t - 1.0f;
            const float ch = 1.0f / 24.0f;
            int h = 0;
            while (t >= ch)
            {
                t -= ch;
                ++h;
            }
            return string.Format("{0:D2}:{1:D2}", h, Mathf.FloorToInt((t * 60.0f) / ch));
        }
    }

    // Awake
    private void OnEnable()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    // Start
    private void Start()
    {
        elapsedDayTimeTime = startTime;
    }

    // Update
    private void Update()
    {
        elapsedDayTimeTime += Time.deltaTime;
        while (elapsedDayTimeTime >= dayTimeTime)
            elapsedDayTimeTime -= dayTimeTime;
    }
}
