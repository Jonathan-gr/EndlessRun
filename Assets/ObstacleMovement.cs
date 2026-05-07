using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private float destroyZ = -10f; // behind player

    void Update()
    {
        // Move at same speed as floor
        transform.position += Vector3.back * GameManager.GameSpeed * Time.deltaTime;

        // Destroy when behind player
        if (transform.position.z < destroyZ)
            Destroy(gameObject);
    }
}