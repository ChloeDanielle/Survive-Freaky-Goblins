using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndGameScreen : MonoBehaviour
{
    public Text survivalTimeText;  // Reference to the UI Text for survival time
    public Text enemiesKilledText;  // Reference to the UI Text for enemies killed

    // Method to display the end game screen and stats
    public void SetupEndGameScreen(float survivalTime, int enemiesKilled)
    {
        gameObject.SetActive(true);  // Show the end game canvas
        survivalTimeText.text = "Survival Time: " + Mathf.Round(survivalTime) + " seconds";
        enemiesKilledText.text = "Enemies Killed: " + enemiesKilled.ToString();
        Time.timeScale = 0f;  // Pause the game
    }

    // Method to restart the game
    public void PlayAgain()
    {
        Time.timeScale = 1f;  // Resume normal time flow
        SceneManager.LoadScene("SampleScene");  // Replace "GameScene" with the name of your gameplay scene
    }
}
