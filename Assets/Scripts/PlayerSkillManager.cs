using UnityEngine;

public class PlayerSkillManager : MonoBehaviour
{
    public GameObject rotatingSword;  // Reference to the Rotating Sword (attached to the Player)
    public GameObject fireballPrefab;  // Reference to the FireBall prefab
    public LightningStrikeManager lightningStrikeManager;  // Reference to the Lightning Strike Manager
    public Transform player;  // Reference to the player's transform
    public GameObject levelUpUI;  // UI for skill selection

    private int swordLevel = 0;               // Track how many times the sword has leveled up
    private int fireballLevel = 0;            // Track how many times the FireBall has leveled up
    private int lightningStrikeLevel = 0;     // Track how many times the Lightning Strike has leveled up
    private float[] swordScales = { 0.5f, 1f, 1.5f };  // Define sword scales for each level

    void Start()
    {
        // Ensure that the rotating sword starts inactive and is ready to be activated
        if (rotatingSword != null)
        {
            rotatingSword.SetActive(false);
        }
    }

    // Called when the player selects the Rotating Sword skill from the level-up UI
    public void ChooseRotatingSword()
    {
        swordLevel++;  // Increment the sword level

        if (swordLevel <= swordScales.Length)
        {
            // Activate and scale the rotating sword based on the player's skill level
            Debug.Log("Activating or Reactivating Rotating Sword at level: " + swordLevel);
            rotatingSword.SetActive(true);  // Ensure the sword is active

            // Update the sword's scale based on the level
            rotatingSword.GetComponent<RotatingSword>().UpdateSwordScale(swordScales[swordLevel - 1]);

            // Optionally remove the sword skill from the level-up UI when max level is reached
            if (swordLevel == swordScales.Length)
            {
                Debug.Log("Rotating Sword max level reached!");
            }
        }

        ResumeGame();  // Unfreeze the game after choosing the skill
    }

    // Called when the player selects the Lightning Strike skill
    public void ChooseLightningStrike()
    {
        lightningStrikeLevel++;  // Increment Lightning Strike level

        if (lightningStrikeManager != null && player != null)
        {
            // Activate or upgrade the Lightning Strike
            ActivateLightningStrike();
        }

        ResumeGame();  // Unfreeze the game after choosing the skill
    }

    // Activate the Lightning Strike
    private void ActivateLightningStrike()
    {
        if (lightningStrikeManager != null)
        {
            // Initialize the lightning strike manager with the player's position and radius
            lightningStrikeManager.Initialize(player);  // Pass the player's position to the strike system
        }
    }

    // Called when the player selects the FireBall skill from the level-up UI
    public void ChooseFireBall()
    {
        fireballLevel++;  // Increment FireBall level
        if (fireballPrefab != null && player != null)
        {
            // Activate FireBall based on the level
            ActivateFireBall(fireballLevel);
        }

        ResumeGame();  // Unfreeze the game after choosing the skill
    }

    // Activate FireBall with the specified level
    private void ActivateFireBall(int level)
    {
        if (fireballPrefab != null)
        {
            // Fireball level 1: Shoot one fireball
            Instantiate(fireballPrefab, player.position, Quaternion.identity);

            if (level >= 2)
            {
                // Fireball level 2: Shoot a second fireball in the opposite direction
                Instantiate(fireballPrefab, player.position, Quaternion.Euler(0, 0, 180));
            }

            if (level == 3)
            {
                // Fireball level 3: Shoot fireballs in top and bottom directions
                Instantiate(fireballPrefab, player.position, Quaternion.Euler(0, 0, 90));
                Instantiate(fireballPrefab, player.position, Quaternion.Euler(0, 0, -90));
            }
        }
    }

    // Resume the game after a skill is chosen (hide the level-up UI and unpause the game)
    private void ResumeGame()
    {
        Time.timeScale = 1f;  // Unfreeze the game
        if (levelUpUI != null)
        {
            levelUpUI.SetActive(false);  // Hide the level-up UI
        }
    }

    // Method to pause the game and show the level-up UI (called when the player levels up)
    public void LevelUp()
    {
        Time.timeScale = 0f;  // Pause the game
        if (levelUpUI != null)
        {
            levelUpUI.SetActive(true);  // Show the level-up UI
        }
    }
}
