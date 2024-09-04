using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

namespace MatchingCards
{
    public class UIManager : MonoBehaviour
    {

        [Header("Score")]
        [SerializeField] private TMP_Text _scoreText;
        [SerializeField] private TMP_Text _highestScoreTxt;
        private int _totalScore;

        [Header("Game Over")]
        [SerializeField] private TMP_Text _totalScoreTxt;
        [SerializeField] private Button _resetButton;
        [SerializeField] private GameObject _gameOverPanel;

        [Header("Turn")]
        [SerializeField] private TMP_Text _turnTxt;
        private int _turnCounter;

        public static Action<int> OnResetGame;
        private SaveManager _saveManager;
        [Inject]
        public void Constructor(SaveManager saveManager)
        {
            _saveManager = saveManager;
        }

        private void Awake()
        {
            _gameOverPanel.SetActive(false);
            _resetButton.onClick.AddListener(() => OnResetGame?.Invoke(_totalScore));
        }
        private void OnEnable()
        {
            GameManager.OnCardMatchedSuccess += UpdateScore;
            GameManager.OnCardsMismatch += UpdateTurnTxt;
            GameManager.OnGameCompleted += ShowGameOver;
        }
        private void OnDisable()
        {
            GameManager.OnCardMatchedSuccess -= UpdateScore;
            GameManager.OnCardsMismatch -= UpdateTurnTxt;
            GameManager.OnGameCompleted -= ShowGameOver;
        }


        private void UpdateTurnTxt()
        {
            _turnTxt.text = "Turn: " + _turnCounter.ToString();
        }
        private void UpdateTurnTxt(int arg1, int arg2)
        {
            _turnCounter++;
            UpdateTurnTxt();
        }



        private void UpdateScore()
        {
            _turnCounter++;
            _totalScore += 1;
            _scoreText.text = "Score: " + _totalScore;
            UpdateTurnTxt();

        }
        public void UpdateHighestScore(int highestScore)
        {
            _highestScoreTxt.text = "Highest Score: " + highestScore;
        }
        private void ShowGameOver()
        {
            _saveManager.SaveScore(_totalScore);
            _gameOverPanel.SetActive(true);
            _totalScoreTxt.text = "Total Score: " + _totalScore;

        }

    }
}