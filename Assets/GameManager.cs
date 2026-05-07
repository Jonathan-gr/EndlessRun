using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static float GameSpeed;
    [SerializeField] private float speedIncreaseRate = 0.5f;

    [SerializeField] private float gameSpeed = 10f;

    private bool gameOver = false;
    void Awake()
    {
        GameSpeed = gameSpeed;
    }

    void Update()
    {
        if (gameOver) return;
        GameSpeed += speedIncreaseRate * Time.deltaTime; // gets faster over time
    }

    public void SetGameOver()
    {
        gameOver = true;
    }
}