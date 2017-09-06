using UnityEngine;
using UnityEngine.SceneManagement;

// Loot mission manager
[RequireComponent(typeof(TimerControllerScript))]
public class LootMissionManagerScript : MonoBehaviour
{
    // Timer controller
    private TimerControllerScript timerController = null;

    // Last day time
    private EDayTime lastDayTime = EDayTime.Noon;

    // Player controller
    private PlayerControllerScript playerController = null;

    // Instance reference
    private static LootMissionManagerScript instance = null;

    // Instance reference
    public static LootMissionManagerScript Instance
    {
        get
        {
            return instance;
        }
    }

    // End day
    private void EndDay()
    {
        if ((DialogueManagerScript.Instance != null) && (playerController != null))
            DialogueManagerScript.Instance.ShowDialogue(playerController.CharacterDialogueAsset, playerController.CharacterName, "I think I should get back home.", blahBlahClip: playerController.CharacterBlahBlahClip);
        if (timerController != null)
            timerController.IsRunning = true;
    }

    // Go home
    public void GoHome()
    {
        SceneManager.LoadScene("FortressScene");
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
    private void Start()
    {
        playerController = GetComponent<PlayerControllerScript>();
        timerController = GetComponent<TimerControllerScript>();
        if ((DialogueManagerScript.Instance != null) && (playerController != null))
            DialogueManagerScript.Instance.ShowDialogue(playerController.CharacterDialogueAsset, playerController.CharacterName, "I hope I can find something useful...", blahBlahClip: playerController.CharacterBlahBlahClip);
    }

    // Update
    private void Update()
    {
        if (TimeManagerScript.Instance != null)
        {
            EDayTime dt = TimeManagerScript.Instance.DayTime;
            if (dt != lastDayTime)
            {
                lastDayTime = dt;
                if (dt == EDayTime.Night)
                    EndDay();
            }
        }
    }
}
