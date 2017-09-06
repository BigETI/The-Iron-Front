using UnityEngine;
using UnityEngine.SceneManagement;

// Game menu manager
public class GameMenuManagerScript : MonoBehaviour
{
    // Start new game
    public void StartNewGame()
    {
        SceneManager.LoadScene("UrbanLootingScene");
    }

    // Show options
    public void ShowOptions()
    {
        SceneManager.LoadScene("OptionsScene");
    }

    // Show credits
    public void ShowCredits()
    {
        SceneManager.LoadScene("CreditsScene");
    }

    // Show main menu
    public void ShowMainMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }

    // Exit game
    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
