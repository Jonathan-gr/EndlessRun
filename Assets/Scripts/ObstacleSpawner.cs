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

    [Header("Log Sub-Spawn Rates")]
    [Range(0f, 1f)] public float leftLogChance = 0.25f;
    [Range(0f, 1f)] public float rightLogChance = 0.25f;
    [Range(0f, 1f)] public float fullLogChance = 0.25f;

    [Range(0f, 1f)] public float coinOnLogChance = 0.5f;

    [Header("Wall Sub-Spawn Rates")]
    [Range(0f, 1f)] public float leftWallChance = 0.2f;
    [Range(0f, 1f)] public float rightWallChance = 0.2f;

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

    public float GameSpeedThreshhold = 25f;
    public float GameSpeedThreshholdDelta = 5f;

    void Awake()
    {
        laneXPositions = new float[] { leftSpawnPoint, 0f, rightSpawnPoint };
        DetermineNextSpawnTime();
    }

    void Update()
    {


        // 1. Use the actual Threshold variable to check
        if (GameManager.GameSpeed > GameSpeedThreshhold)
        {
            // 2. Increase the threshold so this block doesn't run again 
            // until the NEXT milestone (e.g., 25, then 35, then 45...)
            GameSpeedThreshhold += GameSpeedThreshholdDelta;

            // 3. Decrease times but CAP them so they don't go below a playable limit
            // (e.g., never faster than 0.3 seconds)
            minSpawnTime = Mathf.Max(0.1f, minSpawnTime - 0.1f);
            maxSpawnTime = Mathf.Max(0.3f, maxSpawnTime - 0.1f);


        }
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
            float logRandom = Random.value;
            GameObject newLog = Instantiate(logPrefab, new Vector3(0, 0.3f, spawnZ), Quaternion.Euler(0, 0, 90));

            // 1. LEFT LOG
            if (logRandom < leftLogChance)
            {
                newLog.transform.position = new Vector3(-1.07f, 0.3f, spawnZ);
                newLog.transform.localScale = new Vector3(0.5f, 1.79f, 0.3f);
            }
            // 2. RIGHT LOG
            else if (logRandom < leftLogChance + rightLogChance)
            {
                newLog.transform.position = new Vector3(1.07f, 0.3f, spawnZ);
                newLog.transform.localScale = new Vector3(0.5f, 1.79f, 0.3f);
            }
            // 3. FULL LOG (Extra Long)
            else if (logRandom < leftLogChance + rightLogChance + fullLogChance)
            {
                newLog.transform.position = new Vector3(0.04f, 0.3f, spawnZ);
                newLog.transform.localScale = new Vector3(0.5f, 2.89f, 0.3f);
            }
            // 4. REGULAR LOG (Original Size/Pos)
            else
            {
                // These are your "Original" values from your first screenshot
                newLog.transform.position = new Vector3(-1.3f, 0.3f, spawnZ);
                newLog.transform.localScale = new Vector3(0.5f, 0.7f, 0.3f);
            }
            if (Random.value < coinOnLogChance)
            {
                // Use the log's exact position but raise the Y for the jump
                Vector3 coinPos = newLog.transform.position;
                coinPos.y = coinHeight;

                Instantiate(coinPrefab, coinPos, Quaternion.Euler(90f, 0f, 0f));
            }
        }

        // 3. Otherwise, spawn Obstacle
        else
        {
            float wallRandom = Random.value;
            // Spawn at 0.5f height for ground objects
            GameObject newWall = Instantiate(obstaclePrefab, new Vector3(0, 0.5f, spawnZ), Quaternion.identity);

            // 1. LEFT WALL
            if (wallRandom < leftWallChance)
            {
                newWall.transform.position = new Vector3(-1.31f, 0.59f, spawnZ);
                newWall.transform.localScale = new Vector3(3.38f, 4.4f, 0.3f);
            }
            // 2. RIGHT WALL (Mirrored X)
            else if (wallRandom < leftWallChance + rightWallChance)
            {
                newWall.transform.position = new Vector3(1.31f, 0.59f, spawnZ);
                newWall.transform.localScale = new Vector3(3.38f, 4.4f, 0.3f);
            }
            // 3. REGULAR WALL (Original Settings)
            else
            {
                // Use the values from your newest screenshot to keep it "Original"
                newWall.transform.position = new Vector3(-1.8f, 0.59f, spawnZ);
                newWall.transform.localScale = new Vector3(1.18f, 4.4f, 0.3f);
            }
        }
    }
}
