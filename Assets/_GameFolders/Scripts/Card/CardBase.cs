using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MatchingCards
{
    public abstract class CardBase : MonoBehaviour, ICard
    {
        protected CardName _currentCard;
        protected Sprite _cardSprite;
        [SerializeField] protected Sprite _backSprite;
        protected Image _image;
        protected Button _cardButton;
        protected int _cardIndex;
        public Level _level;

        public static Action<CardName, int> OnCardSelected;

        protected virtual void Start()
        {
            _image = GetComponent<Image>();
            _image.sprite = _backSprite;
            _cardButton = GetComponent<Button>();
            _cardButton.onClick.AddListener(OnClickCardButton);
        }

        public abstract void SetCardName(CardName cardName, int cardIndex, Sprite cardSprite);

        public abstract void OnClickCardButton();

        // Method to flip back the card
        public virtual void FlipCardBack(int cardIndex1, int cardIndex2)
        {
            if (_cardIndex == cardIndex1 || _cardIndex == cardIndex2)
            {
                StartCoroutine(FlipCardsBack());
            }
        }

        public virtual void RemoveCard(int index)
        {
            if (_cardIndex == index)
            {
                StartCoroutine(DisappearCard());
            }
        }

        protected IEnumerator FlipCardsBack()
        {
            yield return new WaitForSeconds(_level.FlipDelayTime);
            _image.sprite = _backSprite;
        }

        protected IEnumerator DisappearCard()
        {
            for (float alpha = 1f; alpha >= 0f; alpha -= 0.1f)
            {
                Color color = _image.color;
                color.a = alpha;
                _image.color = color;
                yield return new WaitForSeconds(_level.DisappearCardTime);
            }
            _image.enabled = false;
            _cardButton.enabled = false;
        }
    }







    public enum CardName
    {
        CLUBJACK,
        CLUBACE,
        DIAMONDJACK,
        DIAMONDACE,
        HEARTJACK,
        HEARTACE,
        SPADEACE,
        SPADEJACK,
        JOKER,
    }

}
