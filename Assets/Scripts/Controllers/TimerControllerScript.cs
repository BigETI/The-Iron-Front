using UnityEngine;
using UnityEngine.Events;

// Timer controller
public class TimerControllerScript : MonoBehaviour
{
    // On tick event
    [SerializeField]
    private UnityEvent onTick;

    // Is running
    [SerializeField]
    private bool isRunning = true;

    // Tick time
    [Range(0.0f, 100000.0f)]
    [SerializeField]
    private float tickTime = 1.0f;

    // Repeat
    [SerializeField]
    private bool repeat = true;

    // Elapsed tick time
    private float elapsedTickTime = 0.0f;

    // Is running
    public bool IsRunning
    {
        get
        {
            return isRunning;
        }
        set
        {
            if (isRunning != value)
            {
                if (isRunning)
                    elapsedTickTime = 0.0f;
                isRunning = value;
            }
        }
    }

    // Tick time
    public float TickTime
    {
        get
        {
            return tickTime;
        }
        set
        {
            if (value < 0.0f)
                value = 0.0f;
            tickTime = value;
        }
    }

    // Repeat ticks
    public bool Repeat
    {
        get
        {
            return repeat;
        }
        set
        {
            repeat = value;
        }
    }

    // Update
    private void Update()
    {
        if (isRunning)
        {
            elapsedTickTime += Time.deltaTime;
            while (elapsedTickTime >= tickTime)
            {
                elapsedTickTime -= tickTime;
                onTick.Invoke();
                if (!repeat)
                {
                    IsRunning = false;
                    break;
                }
            }
        }
    }
}
