using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private GameObject obstaclePrefab;
    [SerializeField] private GameObject coinPrefab;

    [Header("Spawn Rates")]
    [Range(0f, 1f)]
    public float coinSpawnChance = 0.3f; // 0.3 = 30% chance for a coin

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


        Vector3 spawnPos = new Vector3(xPos, coinHeight, spawnZ);

        if (Random.value < coinSpawnChance)
        {
            Instantiate(coinPrefab, spawnPos, Quaternion.Euler(90f, 0f, 0f));
        }
        else
        {
            // You can keep obstacles at 0.5f if you want them on the ground
            Vector3 obstaclePos = new Vector3(xPos, 0.5f, spawnZ);
            Instantiate(obstaclePrefab, obstaclePos, Quaternion.identity);
        }
    }

}
