using UnityEngine;
using UnityEngine.Events;

// Trigger
public class TriggerScript : MonoBehaviour
{
    // On start event
    [SerializeField]
    private UnityEvent onStart;

    // Start
    private void Start()
    {
        onStart.Invoke();
    }
}
