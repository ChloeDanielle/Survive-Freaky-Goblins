using UnityEngine;

public class LightningStrike : MonoBehaviour
{
    public int damage = 50;  // Damage dealt by the lightning strike

    private void Start()
    {
        // Destroy the lightning effect after 1 second to clean up the scene
        Destroy(gameObject, 1f);
    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        // Check if the object hit is an enemy
        Enemy enemy = hitInfo.GetComponent<Enemy>();

        if (enemy != null)
        {
            // Deal damage to the enemy
            enemy.TakeDamage(damage);
        }
    }
}
