using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private int _facedOffCard = 0;
    private CardName previousCardType;
    private int previousCardIndex;
    private int score = 0;
    private int matchedPairs = 0;
    private int totalPairs;

    [SerializeField] private float flipBackDelay;

    public static Action<int> OnCardMatched;
    public static Action OnCardMatchedSuccess;
    public static Action OnGameCompleted;
    public static Action<int, int> OnCardsMismatch;
    private bool _cardIsDisplaying;
    private void OnEnable()
    {
        Card.OnCardSelected += CardSelected;
    }

    private void OnDisable()
    {
        Card.OnCardSelected -= CardSelected;
    }

    private void Start()
    {
        totalPairs = (FindObjectsOfType<Card>().Length) / 2;
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
            previousCardType = cardType;
            previousCardIndex = cardIndex;
        }
        else
        {
            if (previousCardType == cardType && previousCardIndex != cardIndex)
            {
                score += 10;
                OnCardMatchedSuccess?.Invoke();

                matchedPairs++;

                if (matchedPairs >= totalPairs)
                {
                    OnGameCompleted?.Invoke();
                }
                OnCardMatched?.Invoke(cardIndex);
                OnCardMatched?.Invoke(previousCardIndex);
            }
            else
            {
                OnCardsMismatch.Invoke(cardIndex, previousCardIndex);
            }

            _facedOffCard = 0;
        }
    }





}
