using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private int _facedOffCard = 0;
    private CardName _previousCardType;
    private int _previousCardIndex;
    private int _matchedPairs = 0;
    private int _totalPairs;

    [SerializeField] private float _flipBackDelay;

    public static Action<int> OnCardMatched;
    public static Action OnCardMatchedSuccess;
    public static Action OnGameCompleted;
    public static Action<int, int> OnCardsMismatch;
    private bool _cardIsDisplaying;

    private void OnEnable()
    {
        Card.OnCardSelected += CardSelected;
        UIManager.OnResetGame+= Restart;
    }

    private void OnDisable()
    {
        Card.OnCardSelected -= CardSelected;
    }

    private void Start()
    {
        _totalPairs = (FindObjectsOfType<Card>().Length) / 2;
    }

    private void CardSelected(CardName cardType, int cardIndex)
    {
        if (_cardIsDisplaying)
        {
            return;
        }
        _facedOffCard++;



        if (_facedOffCard == 1)
        {
            _previousCardType = cardType;
            _previousCardIndex = cardIndex;
        }
        else
        {
            if (_previousCardType == cardType && _previousCardIndex != cardIndex)
            {
                OnCardMatchedSuccess?.Invoke();

                _matchedPairs++;

                if (_matchedPairs >= _totalPairs)
                {
                    OnGameCompleted?.Invoke();
                }
                OnCardMatched?.Invoke(cardIndex);
                OnCardMatched?.Invoke(_previousCardIndex);
            }
            else
            {
                OnCardsMismatch.Invoke(cardIndex, _previousCardIndex);

            }

            _facedOffCard = 0;
        }
    }

    public void Restart(int score)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }




}
