using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private GameObject obstaclePrefab;
    [SerializeField] private float spawnInterval = 2f;
    [SerializeField] private float spawnZ = 30f;      // far end of lane

    public float leftSpawnPoint = -0.3f;
    public float rightSpawnPoint = 0.3f;
    [SerializeField] private float[] laneXPositions; // left, mid, right

    private float timer;
    void Awake()
    {
        laneXPositions = new float[]
        {
        leftSpawnPoint,
        0f,
        rightSpawnPoint
        };
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            timer = 0f;
            SpawnObstacle();
        }
    }

    void SpawnObstacle()
    {
        // Pick a random lane
        int randomLane = Random.Range(0, laneXPositions.Length);
        float xPos = laneXPositions[randomLane];

        Vector3 spawnPos = new Vector3(xPos, 0.5f, spawnZ);
        Instantiate(obstaclePrefab, spawnPos, Quaternion.identity);
    }
}