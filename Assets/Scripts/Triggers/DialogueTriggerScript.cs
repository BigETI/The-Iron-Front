using UnityEngine;

// Dialogue trigger
public class DialogueTriggerScript : MonoBehaviour
{

    // Dialogue
    [SerializeField]
    private DialogueObjectScript dialogue;

    // Start
    private void Start()
    {
        if (DialogueManagerScript.Instance != null)
            DialogueManagerScript.Instance.ShowDialogue(dialogue);
    }
}
