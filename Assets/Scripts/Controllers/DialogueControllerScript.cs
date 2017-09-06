using UnityEngine;
using UnityEngine.UI;

// Dialogue controller
public class DialogueControllerScript : MonoBehaviour
{

    // Sender text
    [SerializeField]
    private Text senderText;

    // Message text
    [SerializeField]
    private Text messageText;

    // Message
    private string message = "";

    // Scaled time
    private bool scaledTime = true;

    // Character time
    private float charTime = 0.0f;

    // Elapsed character time
    private float elapsedCharTime = 0.0f;

    // Message length
    private uint messageLength = 0U;

    // Blah blah clip
    private AudioClip blahBlahClip = null;

    // Fill with values
    public void Fill(string sender, string message, bool scaledTime, float charTime, AudioClip blahBlahClip)
    {
        if (senderText != null)
            senderText.text = sender;
        this.message = message;
        this.charTime = charTime;
        this.blahBlahClip = blahBlahClip;
        messageLength = 0U;
        elapsedCharTime = 0.0f;
        this.scaledTime = scaledTime;
        UpdateVisuals();
    }

    // Update visuals
    private void UpdateVisuals()
    {
        if (messageText != null)
            messageText.text = message.Substring(0, (int)messageLength);
    }

    // Update
    private void Update()
    {
        if (message.Length > messageLength)
        {
            elapsedCharTime += (scaledTime ? Time.deltaTime : Time.unscaledDeltaTime);
            while (elapsedCharTime >= charTime)
            {
                elapsedCharTime -= charTime;
                if (message.Length <= messageLength)
                    break;
                ++messageLength;
                if (AudioManagerScript.Instance != null)
                    AudioManagerScript.Instance.PlaySoundEffect(blahBlahClip);
                UpdateVisuals();
            }
        }
    }
}
