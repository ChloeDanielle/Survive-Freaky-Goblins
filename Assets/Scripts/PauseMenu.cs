using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;  // To track whether the game is paused
    public GameObject pauseMenuUI;  // Reference to the Pause Menu UI

    void Update()
    {
        // Check for the pause key (Escape by default)
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    // Method to resume the game
    public void Resume()
    {
        pauseMenuUI.SetActive(false);  // Hide the pause menu
        Time.timeScale = 1f;  // Resume normal time flow
        GameIsPaused = false;
    }

    // Method to pause the game
    void Pause()
    {
        pauseMenuUI.SetActive(true);  // Show the pause menu
        Time.timeScale = 0f;  // Stop time
        GameIsPaused = true;
    }

    // Method to load the main menu
    public void LoadMainMenu()
    {
        Time.timeScale = 1f;  // Ensure normal time flow
        SceneManager.LoadScene("MainMenuScene");  // Load the Main Menu scene (name it "MainMenu")
    }

    // Method to quit the game (optional)
    public void QuitGame()
    {
        Application.Quit();
    }
}
