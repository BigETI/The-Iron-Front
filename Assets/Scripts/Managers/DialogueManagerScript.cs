using UnityEngine;
using UnityEngine.UI;

// Dialogue manager
public class DialogueManagerScript : MonoBehaviour
{

    // Instance reference
    private static DialogueManagerScript instance = null;

    // Current dialogue
    private DialogueControllerScript currentDialogue = null;

    // Rect transform
    private RectTransform rectTransform = null;

    // Life time
    private float lifeTime = 0.0f;

    // Elapsed life time
    private float elapsedLifeTime = 0.0f;

    // Scaled time
    private bool scaledTime;

    // Instance reference
    public static DialogueManagerScript Instance
    {
        get
        {
            return instance;
        }
    }

    // Show dialogue
    public void ShowDialogue(DialogueObjectScript dialogue, bool scaledTime = true)
    {
        if (dialogue != null)
            ShowDialogue(dialogue.Asset, dialogue.SenderName, dialogue.Message, scaledTime, dialogue.LifeTime, dialogue.CharTime, dialogue.BlahBlahClip);
    }

    // Show dialogue
    public void ShowDialogue(GameObject dialogueAsset, string sender, string message, bool scaledTime = true, float lifeTime = 5.0f, float charTime = 0.0625f, AudioClip blahBlahClip = null)
    {
        this.scaledTime = scaledTime;
        this.lifeTime = lifeTime;
        CloseDialogue();
        if (dialogueAsset != null)
        {
            GameObject go = Instantiate(dialogueAsset);
            if (go != null)
            {
                RectTransform rect_transform = go.GetComponent<RectTransform>();
                DialogueControllerScript dialogue_controller = go.GetComponent<DialogueControllerScript>();
                if ((rect_transform != null) && (dialogue_controller != null))
                {
                    rect_transform.SetParent(rectTransform, false);
                    dialogue_controller.Fill(sender, message, scaledTime, charTime, blahBlahClip);
                    currentDialogue = dialogue_controller;
                }
                else
                    Destroy(go);
            }
        }
    }

    // Close dialogue
    public void CloseDialogue()
    {
        elapsedLifeTime = 0.0f;
        if (currentDialogue != null)
        {
            Destroy(currentDialogue.gameObject);
            currentDialogue = null;
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

    // Update
    private void Update()
    {
        if (currentDialogue != null)
        {
            elapsedLifeTime += scaledTime ? Time.deltaTime : Time.unscaledDeltaTime;
            if (elapsedLifeTime >= lifeTime)
                CloseDialogue();
        }
    }
}
