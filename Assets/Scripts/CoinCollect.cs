using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    public GameObject coinParticlePrefab;
    public int coinBonus = 50;

    // We no longer need to drag this in the Inspector
    private ScoreManager scoreManager;

    private void Start()
    {
        // Automatically find the ScoreManager in the scene
        scoreManager = FindFirstObjectByType<ScoreManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Collect();
        }
    }

    void Collect()
    {
        if (scoreManager != null)
        {
            scoreManager.AddCoinScore(coinBonus);
        }

        Instantiate(coinParticlePrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
