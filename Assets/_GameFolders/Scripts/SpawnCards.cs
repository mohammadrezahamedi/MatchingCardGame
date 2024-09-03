using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCards : MonoBehaviour
{
    [SerializeField] private GameObject _card;
    [SerializeField] private Transform _container;
    private void OnEnable()
    {
        UIManager.OnSpawnCard += SpawnCard;
    }
    private void OnDisable()
    {
        UIManager.OnSpawnCard -= SpawnCard;

    }

    private void SpawnCard(int columnQty)
    {
        int grid = columnQty * columnQty;
        for (int i = 0; i < grid; i++)
        {
            GameObject card = Instantiate(_card, _container);
            card.name = "Card " + i;
        }
    }
}
