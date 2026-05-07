using UnityEngine;

public class CoinPickup : MonoBehaviour
{

    public GameObject coinParticlePrefab;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Collect();
        }
    }

    void Collect()
    {
        Debug.Log("Coin collected!");
        Instantiate(coinParticlePrefab, transform.position, Quaternion.identity);
        // optional: add score

        Destroy(gameObject);
    }
}