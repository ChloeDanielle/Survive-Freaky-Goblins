using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public GameObject tileChunkPrefab;  // Assign the TileChunkPrefab here
    public Transform player;  // Assign the Player transform here
    public float chunkSize = 10f;  // Set the size of each chunk (10x10, etc.)
    public int viewDistance = 2;  // Number of chunks to load around the player

    private Vector2 playerLastPosition;
    private HashSet<Vector2> activeChunks = new HashSet<Vector2>();

    void Start()
    {
        playerLastPosition = new Vector2(
            Mathf.Floor(player.position.x / chunkSize), 
            Mathf.Floor(player.position.y / chunkSize)
        );
        UpdateChunks();
    }

    void Update()
    {
        Vector2 currentPlayerPos = new Vector2(
            Mathf.Floor(player.position.x / chunkSize),
            Mathf.Floor(player.position.y / chunkSize)
        );

        if (currentPlayerPos != playerLastPosition)
        {
            playerLastPosition = currentPlayerPos;
            UpdateChunks();
        }
    }

    void UpdateChunks()
    {
        List<Vector2> chunksToRemove = new List<Vector2>(activeChunks);

        for (int x = -viewDistance; x <= viewDistance; x++)
        {
            for (int y = -viewDistance; y <= viewDistance; y++)
            {
                Vector2 chunkPos = new Vector2(playerLastPosition.x + x, playerLastPosition.y + y);

                if (!activeChunks.Contains(chunkPos))
                {
                    SpawnChunk(chunkPos);
                }
                else
                {
                    chunksToRemove.Remove(chunkPos);
                }
            }
        }

        foreach (Vector2 chunkPos in chunksToRemove)
        {
            DespawnChunk(chunkPos);
        }
    }

    void SpawnChunk(Vector2 chunkPos)
    {
        GameObject newChunk = Instantiate(tileChunkPrefab);
        newChunk.transform.position = new Vector3(chunkPos.x * chunkSize, chunkPos.y * chunkSize, 0);
        newChunk.name = "Chunk_" + chunkPos.x + "_" + chunkPos.y;
        activeChunks.Add(chunkPos);
    }

    void DespawnChunk(Vector2 chunkPos)
    {
        GameObject chunkToRemove = GameObject.Find("Chunk_" + chunkPos.x + "_" + chunkPos.y);
        if (chunkToRemove != null)
        {
            Destroy(chunkToRemove);
            activeChunks.Remove(chunkPos);
        }
    }
}
