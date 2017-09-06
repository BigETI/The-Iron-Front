using UnityEngine;
using UnityEngine.UI;

// Notification controller
public class NotificationControllerScript : MonoBehaviour
{
    // Title text
    [SerializeField]
    private Text titleText;

    // Message text
    [SerializeField]
    private Text messageText;

    // Background image
    private Image backgroundImage;

    // Panel animator
    private Animator panelAnimator;

    // Notify
    public void Notify(string title, string message, Color foregroundColor, Color backgroundColor, float charTime, AudioClip blahblahClip)
    {
        if (titleText != null)
        {
            titleText.text = title;
            titleText.color = foregroundColor;
        }
        if (messageText != null)
        {
            messageText.text = message;
            messageText.color = foregroundColor;
        }
        if (backgroundImage == null)
            backgroundImage = GetComponent<Image>();
        if (backgroundImage != null)
            backgroundImage.color = backgroundColor;
        if (panelAnimator != null)
            panelAnimator.Play("Notify");
    }
}
