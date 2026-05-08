using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static float GameSpeed;
    [SerializeField] private float speedIncreaseRate = 0.5f;
    public static GameManager Instance;
    [SerializeField] private float gameSpeed = 10f;

    private bool gameOver = false;
    void Awake()
    {
        GameSpeed = gameSpeed;
        Instance = this;
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

    public void RestartGame()
    {
        Debug.Log("restarting game");
        // 1. Reset time so the game actually moves
        Time.timeScale = 1f;

        // 2. Reload the currently active scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}