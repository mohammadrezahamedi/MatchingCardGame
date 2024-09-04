using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MatchingCards
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] private AudioSource _audio;
        [SerializeField] public AudioClip _successClip;
        [SerializeField] public AudioClip _gameEndClip;
        [SerializeField] public AudioClip _mismatchClip;
        [SerializeField] public AudioClip _cardFlippingClip;

        private void OnEnable()
        {
            GameManager.OnCardMatchedSuccess += MatchedSfx;
            GameManager.OnCardsMismatch += MismatchedSfx;
            GameManager.OnGameCompleted += GameOverSfx;
        }

        private void OnDisable()
        {
            GameManager.OnCardMatchedSuccess -= MatchedSfx;
            GameManager.OnGameCompleted -= GameOverSfx;
            GameManager.OnCardsMismatch -= MismatchedSfx;
        }

        private void GameOverSfx()
        {
            _audio.PlayOneShot(_gameEndClip);
        }
        private void MatchedSfx()
        {
            _audio.PlayOneShot(_successClip);
        }
        private void MismatchedSfx(int a, int b)
        {
            _audio.PlayOneShot(_mismatchClip);
        }
        public void CardFlippingSFX()
        {
            _audio.PlayOneShot(_cardFlippingClip);
        }
    }
}