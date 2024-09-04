using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Text scoreText;
    [SerializeField] private GameObject gameOverPanel; 

    private void OnEnable()
    {
        GameManager.OnScoreUpdated += UpdateScore;
        GameManager.OnGameCompleted += ShowGameOver;
    }

    private void OnDisable()
    {
        GameManager.OnScoreUpdated -= UpdateScore;
        GameManager.OnGameCompleted -= ShowGameOver;
    }

    private void UpdateScore(int newScore)
    {
        scoreText.text = "Score: " + newScore;
    }

    private void ShowGameOver()
    {
       //TODO: GameOverPanel
    }
}
