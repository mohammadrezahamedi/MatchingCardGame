using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField]private AudioSource _audio;
    [SerializeField] public AudioClip _successClip;
    [SerializeField] public AudioClip _gameEndClip;
    [SerializeField] public AudioClip _mismatchClip;

    private void OnEnable()
    {
        GameManager.OnCardMatchedSuccess += MatchedSoundEffect;
        GameManager.OnCardsMismatch += MismatchedSoundEffect;
        GameManager.OnGameCompleted += GameOverSoundEffect;
    }

    private void OnDisable()
    {
        GameManager.OnCardMatchedSuccess -= MatchedSoundEffect;
        GameManager.OnGameCompleted -= GameOverSoundEffect;
        GameManager.OnCardsMismatch -= MismatchedSoundEffect;
    }

    private void GameOverSoundEffect()
    {
        _audio.PlayOneShot(_gameEndClip);
    }
    private void MatchedSoundEffect()
    {
        _audio.PlayOneShot(_successClip);
    }
    private void MismatchedSoundEffect(int a,int b)
    {
        _audio.PlayOneShot(_mismatchClip);
    }
}
