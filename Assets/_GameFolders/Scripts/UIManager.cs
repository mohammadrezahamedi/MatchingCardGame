using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    [Header("Score")]
    [SerializeField] private TMP_Text _scoreText;
    private int _totalScore;


    [Header("Game Over")]
    [SerializeField] private TMP_Text _totalScoreTxt;
    [SerializeField] private Button _resetButton;
    [SerializeField] private GameObject _gameOverPanel;


    private void Awake()
    {
        _gameOverPanel.SetActive(false);
        _resetButton.onClick.AddListener(Restart);
    }
    private void OnEnable()
    {
        GameManager.OnCardMatchedSuccess += UpdateScore;
        GameManager.OnGameCompleted += ShowGameOver;
    }

    private void OnDisable()
    {
        GameManager.OnCardMatchedSuccess -= UpdateScore;
        GameManager.OnGameCompleted -= ShowGameOver;
    }

    private void UpdateScore()
    {
        _totalScore += 10;
        _scoreText.text = "Score: " + _totalScore;
    }

    private void ShowGameOver()
    {
        _gameOverPanel.SetActive(true);
        _totalScoreTxt.text= "Total Score: " + _totalScore;
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
