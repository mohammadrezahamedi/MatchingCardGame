using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCards : MonoBehaviour
{
    [SerializeField] private Card _cardPrefab;
    [SerializeField] private Transform _container;
    [SerializeField] private Level _level;

    private void Awake()
    {
        SpawnAllCards();
    }

    private void SpawnAllCards()
    {
        for (int row = 0; row < _level.CardColumnQty; row++)
        {
            for (int col = 0; col < _level.CardRowQty; col++)
            {

                Card card = Instantiate(_cardPrefab, _container);
                card.name = "Card " + (row * _level.CardColumnQty + col);

              //  card.SetCardName();
            }
        }
    }
}
