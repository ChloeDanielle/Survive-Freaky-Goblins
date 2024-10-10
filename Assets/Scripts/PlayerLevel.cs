using UnityEngine;
using UnityEngine.UI;

public class PlayerLevel : MonoBehaviour
{
    public int currentLevel = 1;
    public int currentExperience = 0;
    public int experienceToNextLevel = 100;
    public Slider experienceSlider; // UI Slider to show experience progress
    public Text levelText; // UI Text to display current level
    private PlayerHealth playerHealth; // Reference to PlayerHealth component
    public PlayerSkillManager skillManager; // Reference to the PlayerSkillManager

    void Start()
    {
        // Set the max value for the experience slider
        experienceSlider.maxValue = experienceToNextLevel;
        experienceSlider.value = currentExperience;

        // Initialize the level text UI
        levelText.text = "Level: " + currentLevel;

        // Find and cache the PlayerHealth component
        playerHealth = GetComponent<PlayerHealth>();
    }

    // Call this method to gain experience
    public void GainExperience(int amount)
    {
        currentExperience += amount;
        experienceSlider.value = currentExperience;

        // Level up if experience is greater than or equal to the required amount
        if (currentExperience >= experienceToNextLevel)
        {
            LevelUp();
        }
    }

    // Level up method
    void LevelUp()
    {
        currentLevel++;  // Increment the player's level
        currentExperience -= experienceToNextLevel;  // Carry over any extra experience
        experienceToNextLevel += 50;  // Increase the experience required for the next level

        // Update the slider's max value and current experience
        experienceSlider.maxValue = experienceToNextLevel;
        experienceSlider.value = currentExperience;

        // Update the level text
        levelText.text = "Level: " + currentLevel;

        // Restore the player's health on level up
        if (playerHealth != null)
        {
            playerHealth.RestoreFullHealth();
        }

        // Call the LevelUp method from PlayerSkillManager to show the skill selection UI
        if (skillManager != null)
        {
            skillManager.LevelUp();  // Trigger the skill selection UI
        }
        else
        {
            Debug.LogError("PlayerSkillManager is not assigned in PlayerLevel.");
        }
    }
}
