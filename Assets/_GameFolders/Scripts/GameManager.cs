using System;
using System.Collections;
using UnityEngine;

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
    public static Action<int> OnScoreUpdated;
    public static Action OnGameCompleted;
    public static Action<int, int> OnCardsMismatch;
    private bool _isDisplaying;
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
        if (_isDisplaying)
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
                OnScoreUpdated?.Invoke(score);

                matchedPairs++;

                if (matchedPairs >= totalPairs)
                {
                    OnGameCompleted?.Invoke();
                }

                StartCoroutine(FlipCardsBack(cardIndex, previousCardIndex, true));
            }
            else
            {
                StartCoroutine(FlipCardsBack(cardIndex, previousCardIndex, false));
            }

            _facedOffCard = 0;
        }
    }

    private IEnumerator FlipCardsBack(int cardIndex1, int cardIndex2, bool matched)
    {
        if (matched)
        {
            yield return new WaitForSeconds(flipBackDelay);

            OnCardMatched?.Invoke(cardIndex1);
            OnCardMatched?.Invoke(cardIndex2);
        }
        else
        {
            _isDisplaying = true;
            yield return new WaitForSeconds(flipBackDelay);
            OnCardsMismatch?.Invoke(cardIndex1, cardIndex2);
            _isDisplaying = false;

        }
    }
}
