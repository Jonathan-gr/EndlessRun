using UnityEngine;
using TMPro; // Required for TextMeshPro

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText; // Drag ScoreText here
    public float scoreMultiplier = 10f; // How fast the score goes up

    private float currentScore = 0f;
    private bool isPlayerAlive = true;

    void Update()
    {
        if (isPlayerAlive)
        {
            // Increase score based on time (constant run)
            currentScore += Time.deltaTime * scoreMultiplier;

            // Update the text (0 means no decimals)
            scoreText.text = "SCORE: " + Mathf.FloorToInt(currentScore).ToString();
        }
    }

    // Call this from your PlayerWallCollision script when the player dies
    public void StopScore()
    {
        isPlayerAlive = false;
    }

    // Call this when picking up a coin
    public void AddCoinScore(int amount)
    {
        currentScore += amount;
    }
}
