using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Method to start the game (loads the gameplay scene)
    public void StartGame()
    {
        SceneManager.LoadScene("SampleScene");  // Replace "GameScene" with the name of your gameplay scene
    }

    // Method to display about information (optional, create your UI as needed)
    public void ShowAbout()
    {
        // Optionally, display some UI with information about the game
        Debug.Log("Showing About information.");
    }

    // Method to quit the game
    public void QuitGame()
    {
        Application.Quit();
    }
}
