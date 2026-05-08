using UnityEngine;

public class PlayerWallCollision : MonoBehaviour
{
    [SerializeField] private GameObject loseButton; // Drag Button here

    [SerializeField] private GameObject starPrefab; // Drag Button here

    public ScoreManager scoreManager;
    private bool isDead = false;

    private void OnCollisionEnter(Collision collision)


    {

        // Ensure your walls are tagged "Wall"
        if (collision.gameObject.CompareTag("Wall") && !isDead || collision.gameObject.CompareTag("Log") && !isDead)
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;
        scoreManager.StopScore();

        // 1. Show the button
        if (loseButton != null)
        {
            loseButton.SetActive(true);

            // 2. Trigger the shake effect
            ShakeButton shakeScript = loseButton.GetComponent<ShakeButton>();
            if (shakeScript != null)
            {
                shakeScript.StartShaking();
            }
        }
        Instantiate(starPrefab, transform.position + new Vector3(1, 0, 0), Quaternion.identity);
        Instantiate(starPrefab, transform.position + new Vector3(-1, 0, 0), Quaternion.identity);

        // 3. Freeze the game world
        Time.timeScale = 0f;
    }
}
