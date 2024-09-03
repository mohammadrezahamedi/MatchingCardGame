using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    [SerializeField] private CardName _cardName;

    void Start()
    {
        
    }

    void Update()
    {

    }
}

public enum CardName
{
    DUMMY,
    HEART,
    DIAMOND,
    CLUB,
    SPADE
}