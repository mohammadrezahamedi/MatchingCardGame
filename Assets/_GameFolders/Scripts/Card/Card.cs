using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace MatchingCards
{
    public class Card : CardBase
    {
        private void OnEnable()
        {
            GameManager.OnCardMatched += RemoveCard;
            GameManager.OnCardsMismatch += FlipCardBack;
        }

        private void OnDisable()
        {
            GameManager.OnCardMatched -= RemoveCard;
            GameManager.OnCardsMismatch -= FlipCardBack;
        }

        public override void SetCardName(CardName cardName, int cardIndex, Sprite cardSprite)
        {
            _currentCard = cardName;
            _cardSprite = cardSprite;
            _cardIndex = cardIndex;
        }

        public override void OnClickCardButton()
        {
            _image.sprite = _cardSprite;
            OnCardSelected?.Invoke(_currentCard, _cardIndex);
        }

        public override void FlipCardBack(int cardIndex1, int cardIndex2)
        {
            base.FlipCardBack(cardIndex1, cardIndex2); // Call base method to handle the flipping
        }

        public override void RemoveCard(int index)
        {
            base.RemoveCard(index); // Use the base class logic for removing cards
        }
    }
}