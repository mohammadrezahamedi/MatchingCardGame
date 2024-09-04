using UnityEngine;

namespace MatchingCards
{
    public interface ICard
    {
        void SetCardName(CardName cardName, int cardIndex, Sprite cardSprite);
        void OnClickCardButton();
        void FlipCardBack(int a,int b);
        void RemoveCard(int index);
    }
}