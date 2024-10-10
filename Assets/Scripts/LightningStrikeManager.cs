using UnityEngine;
using System.Collections;

public class LightningStrikeManager : MonoBehaviour
{
    public float strikeInterval = 5f;   // Time between strikes (5 seconds)
    public float strikeRadius = 5f;     // Radius around the player where the strike can occur
    public int damage = 50;             // Damage dealt by the strike
    public GameObject strikeEffectPrefab;  // Reference to the lightning strike effect (visual effect)
    private Transform player;           // Reference to the player's transform

    // Initialize the lightning strike manager with the player's reference
    public void Initialize(Transform playerTransform)
    {
        player = playerTransform;  // Set the player's transform
        StartCoroutine(LightningStrikeRoutine());  // Start the strike routine
    }

    private IEnumerator LightningStrikeRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(strikeInterval);  // Wait for the interval (5 seconds)

            // Call the strike method
            TriggerStrike();
        }
    }

    private void TriggerStrike()
    {
        // Pick a random point within the radius around the player
        Vector2 randomPoint = (Vector2)player.position + Random.insideUnitCircle * strikeRadius;

        // Instantiate the lightning strike effect at the random position
        GameObject strikeEffect = Instantiate(strikeEffectPrefab, randomPoint, Quaternion.identity);

        // Destroy the lightning effect after a short duration
        Destroy(strikeEffect, 1f);  // Destroy the effect after 1 second

        // Check for enemies in the strike area
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(randomPoint, 1f);  // 1f is the strike area radius

        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy.CompareTag("Enemy"))  // Check if it's an enemy
            {
                enemy.GetComponent<Enemy>().TakeDamage(damage);  // Deal damage to the enemy
            }
        }
    }

    // To visualize the strike radius in the Unity editor
    void OnDrawGizmosSelected()
    {
        if (player == null) return;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(player.position, strikeRadius);  // Draw the strike radius around the player
    }
}
