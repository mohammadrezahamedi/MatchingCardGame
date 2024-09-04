using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCards : MonoBehaviour
{
    [SerializeField] private Card _cardPrefab;
    [SerializeField] private Transform _container;
    [SerializeField] private Level _level;
    [SerializeField] private float _spacing = 80;
    [SerializeField] private Sprite[] _cardFace;
    private int _cardPickNumber;

    private List<int> _shuffledCardIndices;

    private void Awake()
    {
        _cardFace = Resources.LoadAll<Sprite>("Cards");

        int totalCards = _level.Row * _level.Column;
        _cardPickNumber = totalCards / 2;

        _shuffledCardIndices = CreateAndShuffleCards(totalCards);

        SpawnAllCards();
    }

    private List<int> CreateAndShuffleCards(int totalCards)
    {
        List<int> cardIndices = new List<int>();

        for (int i = 0; i < _cardPickNumber; i++)
        {
            cardIndices.Add(i);
            cardIndices.Add(i);
        }

        if (totalCards % 2 != 0)
        {
            cardIndices.Add(_cardFace.Length - 1); // Joker is assumed to be the last sprite
        }

        // Shuffle the list using Fisher-Yates shuffle
        for (int i = cardIndices.Count - 1; i > 0; i--)
        {
            int randomIndex = UnityEngine.Random.Range(0, i + 1);
            int temp = cardIndices[i];
            cardIndices[i] = cardIndices[randomIndex];
            cardIndices[randomIndex] = temp;
        }

        return cardIndices;
    }

    private void SpawnAllCards()
    {
        int index = 0;
        for (int i = 0; i < _level.Row; i++)
        {
            for (int j = 0; j < _level.Column; j++)
            {
                Vector3 cardPosition = new Vector3(SpawnCardsFromCenter().x + j * _spacing, SpawnCardsFromCenter().y - i * _spacing, 0);

                Card card = Instantiate(_cardPrefab, _container);
                card.transform.localPosition = cardPosition;
                int cardIndex = (i * _level.Column + j);
                card.name = "Card " + cardIndex;

                int cardTypeIndex = _shuffledCardIndices[index];
                card.SetCardName((CardName)cardTypeIndex, cardIndex, _cardFace[cardTypeIndex]);
                index++;
            }
        }
    }

    private Vector2 SpawnCardsFromCenter()
    {
        float gridWidth = (_level.Column - 1) * _spacing;
        float gridHeight = (_level.Row - 1) * _spacing;
        Vector3 startPosition = new Vector3(-gridWidth / 2, gridHeight / 2, 0);
        return startPosition;
    }
}
