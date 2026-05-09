using UnityEngine;

public class PlayerWallCollision : MonoBehaviour
{
    [SerializeField] private GameObject loseButton;
    [SerializeField] private GameObject GameOverText;
    [SerializeField] private GameObject starPrefab;

    [SerializeField] private GameObject wallBreakPrefab;
    [SerializeField] private AudioSource musicSource;

    public ScoreManager scoreManager;
    private PlayerInvincibility invincibility;
    private bool isDead = false;

    private void Start()
    {
        invincibility = GetComponent<PlayerInvincibility>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        bool hitWall = collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Log");

        if (!hitWall || isDead) return;
        PlayerAudio.Instance.PlayCrashSound();
        if (invincibility != null && invincibility.IsInvincible)
        {
            Vector3 wallPosition = collision.transform.position;
            Destroy(collision.gameObject);
            Instantiate(wallBreakPrefab, wallPosition, Quaternion.identity);

            Rigidbody rb = GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.linearVelocity = new Vector3(0f, 0f, rb.linearVelocity.z);
                rb.angularVelocity = Vector3.zero;
            }
        }
        else
        {

            Die();
        }
    }

    void Die()
    {
        isDead = true;
        scoreManager.StopScore();

        if (loseButton != null)
        {
            loseButton.SetActive(true);
            ShakeButton shakeScript = loseButton.GetComponent<ShakeButton>();
            if (shakeScript != null) shakeScript.StartShaking();
        }

        musicSource.Stop();
        GameOverText.SetActive(true);
        Instantiate(starPrefab, new Vector3(1.5f, 1.5f, -6), Quaternion.identity);
        Instantiate(starPrefab, new Vector3(-1.5f, 1.5f, -6), Quaternion.identity);

        Time.timeScale = 0f;
    }
}