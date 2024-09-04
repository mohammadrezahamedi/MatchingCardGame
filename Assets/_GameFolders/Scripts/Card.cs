using System;
using UnityEngine;
using UnityEngine.UI;


public class Card : MonoBehaviour
{
    [SerializeField] private CardName _currentCard;
    [SerializeField] private Sprite _backSprite;
    private Sprite _cardSprite;
    private Image _image;
    private Button _cardButton;
    private int _cardIndex;

    public static Action<CardName, int> OnCardSelected;

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

    private void Start()
    {
        _image = GetComponent<Image>();
        _image.sprite = _backSprite;
        _cardButton = GetComponent<Button>();
        _cardButton.onClick.AddListener(OnClickCardButton);
    }

    public void SetCardName(CardName cardName, int cardIndex, Sprite cardSprite)
    {
        _currentCard = cardName;
        _cardSprite = cardSprite;
        _cardIndex = cardIndex;
    }

    private void OnClickCardButton()
    {
        _image.sprite = _cardSprite;
        OnCardSelected?.Invoke(_currentCard, _cardIndex);
    }

    private void RemoveCard(int index)
    {
        if (_cardIndex == index)
        {
            Destroy(gameObject);
        }
    }

    private void FlipCardBack(int cardIndex1, int cardIndex2)
    {
        if (_cardIndex == cardIndex1 || _cardIndex == cardIndex2)
        {
            _image.sprite = _backSprite;
        }
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
    JOKER,
    SPADEACE,
    SPADEJACK,
}
