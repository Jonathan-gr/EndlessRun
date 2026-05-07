using UnityEngine;

public class FloorScroller : MonoBehaviour
{

    [SerializeField] private float resetZPosition = 30f;  // spawn ahead of player
    [SerializeField] private float destroyZPosition = -10f; // behind player

    void Update()
    {
        // Move tile toward player
        transform.position += Vector3.back * GameManager.GameSpeed * Time.deltaTime;

        // When tile passes behind player, teleport to front
        if (transform.position.z < destroyZPosition)
        {
            transform.position = new Vector3(
                transform.position.x,
                transform.position.y,
                resetZPosition
            );
        }
    }
}