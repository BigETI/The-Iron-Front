using UnityEngine;
using UnityEngine.SceneManagement;

// Fortress mission manager
[RequireComponent(typeof(TimerControllerScript))]
public class FortressMissionManagerScript : MonoBehaviour
{
    // Timer controller
    private TimerControllerScript timerController = null;

    // Player controller
    private PlayerControllerScript playerController = null;

    // Instance reference
    private static FortressMissionManagerScript instance = null;

    // Instance reference
    public static FortressMissionManagerScript Instance
    {
        get
        {
            return instance;
        }
    }

    // End wave
    public void EndWave()
    {
        if ((DialogueManagerScript.Instance != null) && (playerController != null))
            DialogueManagerScript.Instance.ShowDialogue(playerController.CharacterDialogueAsset, playerController.CharacterName, "I hope these mechanical nuts won't come back.", blahBlahClip: playerController.CharacterBlahBlahClip);
        if (timerController != null)
            timerController.IsRunning = true;
    }

    // Go out
    public void GoOut()
    {
        SceneManager.LoadScene("UrbanLootingScene");
    }

    // Show main menu
    public void ShowMainMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }

    // On enable
    private void OnEnable()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    // On disable
    private void OnDisable()
    {
        if (instance == this)
            instance = null;
    }

    // Start
    void Start()
    {
        timerController = GetComponent<TimerControllerScript>();
        playerController = FindObjectOfType<PlayerControllerScript>();
    }
}
