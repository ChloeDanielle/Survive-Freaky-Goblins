using UnityEngine;
using System.Collections.Generic;

public class TileChunkManager : MonoBehaviour
{
    public Transform player;                  // Reference to the player's Transform
    public GameObject tileChunkPrefab;        // Prefab of the tile chunk
    public float chunkSize = 10f;             // Size of one tile chunk (for example, 10x10 units)
    public int loadDistance = 3;              // How many chunks around the player should be loaded (3x3 grid)
    public float destroyDistance = 40f;       // Distance beyond which chunks will be destroyed

    private Dictionary<Vector2, GameObject> activeChunks = new Dictionary<Vector2, GameObject>();  // To store active chunks
    private Vector2 playerChunkCoord;         // Current chunk coordinate of the player

    void Start()
    {
        // Spawn initial chunks around the player
        UpdateChunks();
    }

    void Update()
    {
        // Calculate the player's current chunk coordinate
        Vector2 newPlayerChunkCoord = new Vector2(
            Mathf.Floor(player.position.x / chunkSize),
            Mathf.Floor(player.position.y / chunkSize)
        );

        // If the player has moved to a new chunk, update the chunks
        if (newPlayerChunkCoord != playerChunkCoord)
        {
            playerChunkCoord = newPlayerChunkCoord;
            UpdateChunks();
        }
    }

    void UpdateChunks()
    {
        // Calculate which chunks should be active (within loadDistance)
        for (int x = -loadDistance; x <= loadDistance; x++)
        {
            for (int y = -loadDistance; y <= loadDistance; y++)
            {
                Vector2 chunkCoord = new Vector2(playerChunkCoord.x + x, playerChunkCoord.y + y);
                
                // If the chunk is not already active, spawn it
                if (!activeChunks.ContainsKey(chunkCoord))
                {
                    Vector3 chunkPosition = new Vector3(chunkCoord.x * chunkSize, chunkCoord.y * chunkSize, 0);
                    GameObject newChunk = Instantiate(tileChunkPrefab, chunkPosition, Quaternion.identity);
                    activeChunks.Add(chunkCoord, newChunk);
                }
            }
        }

        // Remove chunks that are too far away
        List<Vector2> chunksToRemove = new List<Vector2>();
        foreach (var chunk in activeChunks)
        {
            // If the chunk is too far from the player, mark it for removal
            if (Vector3.Distance(player.position, chunk.Value.transform.position) > destroyDistance)
            {
                Destroy(chunk.Value);  // Destroy the chunk
                chunksToRemove.Add(chunk.Key);  // Mark the chunk for removal from the dictionary
            }
        }

        // Remove the chunks that are marked
        foreach (var chunkCoord in chunksToRemove)
        {
            activeChunks.Remove(chunkCoord);
        }
    }
}
