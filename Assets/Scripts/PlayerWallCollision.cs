using UnityEngine;

public class PlayerWallCollision : MonoBehaviour
{
    private bool isDead = false;

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Wall") && !isDead)
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;

        // Stop everything
        Time.timeScale = 0f;
    }
}