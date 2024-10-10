using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;  // The enemy prefab to spawn
    public Transform player;        // The player's transform
    public float spawnRadius = 10f; // Radius around the player where enemies spawn
    public float spawnRate = 2f;    // Time between each enemy spawn

    void Start()
    {
        InvokeRepeating("SpawnEnemy", 1f, spawnRate); // Repeatedly call SpawnEnemy
    }

    void SpawnEnemy()
    {
        // Generate a random direction and position around the player
        Vector2 spawnDirection = Random.insideUnitCircle.normalized;
        Vector3 spawnPosition = player.position + (Vector3)(spawnDirection * Random.Range(spawnRadius, spawnRadius + 5f));

        // Spawn the enemy at the calculated position
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }
}
