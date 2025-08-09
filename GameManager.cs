using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public float gameSpeed = 5f;
    public int score = 0;
    public Text scoreText;
    public GameObject gameOverPanel;
    public Text highScoreText;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        UpdateScoreUI();
        gameOverPanel.SetActive(false);
        LoadHighScore();
    }

    void Update()
    {
        gameSpeed += Time.deltaTime * 0.02f; // زيادة سرعة اللعبة تدريجيًا
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreUI();
    }

    void UpdateScoreUI()
    {
        if (scoreText != null) scoreText.text = "Score: " + score;
    }

    public void GameOver()
    {
        Time.timeScale = 0f;
        gameOverPanel.SetActive(true);
        SaveHighScore();
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void SaveHighScore()
    {
        int hs = PlayerPrefs.GetInt("HighScore", 0);
        if (score > hs) PlayerPrefs.SetInt("HighScore", score);
        if (highScoreText != null) highScoreText.text = "High: " + PlayerPrefs.GetInt("HighScore", 0);
    }

    void LoadHighScore()
    {
        if (highScoreText != null) highScoreText.text = "High: " + PlayerPrefs.GetInt("HighScore", 0);
    }
}