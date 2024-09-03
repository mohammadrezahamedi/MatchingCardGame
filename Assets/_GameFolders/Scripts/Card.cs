using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    [SerializeField] private CardName _cardName;
    [SerializeField] private Sprite _dummySprite;
    [SerializeField] private Sprite[] _cardFace;
    private Image _image;
  
    private void Start()
    {
        _image = GetComponent<Image>();
        _image.sprite = _dummySprite;
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
