using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public Slider healthSlider;  // Attach a UI Slider to represent health
    public EndGameScreen endGameScreen;  // Reference to the End Game screen
    public float survivalTime;  // To track the player's survival time
    public int enemiesKilled;  // To track the number of enemies killed

    void Start()
    {
        currentHealth = maxHealth;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
        survivalTime = 0f;
        enemiesKilled = 0;

        // Automatically find the EndGameScreen in the scene if not assigned in the Inspector
        if (endGameScreen == null)
        {
            endGameScreen = FindObjectOfType<EndGameScreen>();
        }
    }

    void Update()
    {
        // Track survival time as long as the player is alive
        if (currentHealth > 0)
        {
            survivalTime += Time.deltaTime;
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthSlider.value = currentHealth;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Player Died");

        if (endGameScreen != null)
        {
            Debug.Log("EndGameScreen found and setting up the screen.");
            // Show the end game screen and pass survival time and enemies killed
            endGameScreen.SetupEndGameScreen(survivalTime, enemiesKilled);
        }
        else
        {
            Debug.LogError("EndGameScreen is not assigned or found.");
        }

        gameObject.SetActive(false);  // Disable the player object
    }

    // Method to restore full health
    public void RestoreFullHealth()
    {
        currentHealth = maxHealth;
        healthSlider.value = currentHealth;
    }

    // Method to increment enemy kill count
    public void AddKill()
    {
        enemiesKilled++;
    }
}
