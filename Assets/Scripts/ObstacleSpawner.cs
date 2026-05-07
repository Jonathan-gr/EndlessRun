using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private GameObject obstaclePrefab;
    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private GameObject logPrefab; // Drag your log here

    [Header("Spawn Rates")]
    [Range(0f, 1f)] public float coinSpawnChance = 0.2f; // 20%
    [Range(0f, 1f)] public float logSpawnChance = 0.2f;  // 20%
    // Remaining 60% will be obstacles

    [Header("Timing")]
    [SerializeField] private float minSpawnTime = 1f;
    [SerializeField] private float maxSpawnTime = 3f;
    private float nextSpawnTime;

    [Header("Lanes")]
    [SerializeField] private float spawnZ = 30f;
    public float leftSpawnPoint = -0.3f;
    public float rightSpawnPoint = 0.3f;
    private float[] laneXPositions;

    private float timer;
    public float coinHeight = 1.5f;

    void Awake()
    {
        laneXPositions = new float[] { leftSpawnPoint, 0f, rightSpawnPoint };
        DetermineNextSpawnTime();
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= nextSpawnTime)
        {
            timer = 0f;
            SpawnRandomObject();
            DetermineNextSpawnTime();
        }
    }

    void DetermineNextSpawnTime()
    {
        nextSpawnTime = Random.Range(minSpawnTime, maxSpawnTime);
    }

    void SpawnRandomObject()
    {
        int randomLane = Random.Range(0, laneXPositions.Length);
        float xPos = laneXPositions[randomLane];

        float randomVal = Random.value;

        // 1. Check for Coin
        if (randomVal < coinSpawnChance)
        {
            Vector3 spawnPos = new Vector3(xPos, coinHeight, spawnZ);
            Instantiate(coinPrefab, spawnPos, Quaternion.Euler(90f, 0f, 0f));
        }
        // 2. Check for Log (Rotation Z = 90)
        else if (randomVal < coinSpawnChance + logSpawnChance)
        {
            Vector3 spawnPos = new Vector3(xPos, 0.3f, spawnZ);
            Instantiate(logPrefab, spawnPos, Quaternion.Euler(0f, 0f, 90f));
        }
        // 3. Otherwise, spawn Obstacle
        else
        {
            Vector3 spawnPos = new Vector3(xPos, 0.5f, spawnZ);
            Instantiate(obstaclePrefab, spawnPos, Quaternion.identity);
        }
    }
}
