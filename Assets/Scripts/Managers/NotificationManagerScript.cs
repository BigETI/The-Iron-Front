using UnityEngine;

// Notification manager
public class NotificationManagerScript : MonoBehaviour
{
    // Notification panel asset
    [SerializeField]
    private GameObject notificationPanelAsset;

    // Instance reference
    private static NotificationManagerScript instance = null;

    // Rect transform
    private RectTransform rectTransform;

    // Instance reference
    public static NotificationManagerScript Instance
    {
        get
        {
            return instance;
        }
    }

    // Notify
    public void Notify(string title, string message, Color foregroundColor, Color backgroundColor, float charTime = 0.0625f, AudioClip blahblahClip = null)
    {
        if (notificationPanelAsset != null)
        {
            GameObject go = Instantiate(notificationPanelAsset);
            if (go != null)
            {
                RectTransform rect_transform = go.GetComponent<RectTransform>();
                NotificationControllerScript notification_controller = go.GetComponent<NotificationControllerScript>();
                if ((rect_transform != null) && (notification_controller != null))
                {
                    rect_transform.SetParent(rectTransform, false);
                    notification_controller.Notify(title, message, foregroundColor, backgroundColor, charTime, blahblahClip);
                }
                else
                    Destroy(go);
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
        }
        else
            Destroy(gameObject);
    }

    // Start
    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }
}
