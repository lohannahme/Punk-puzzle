using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LetterButton : MonoBehaviour
{
    [SerializeField] private char _letter;
    [SerializeField] private float _pressTime;

    private float _minusY = .335f;

    private Vector3 _position;
    public char Letter { get => _letter; set => _letter = value; }

    public static Action<char> OnSendLetter;


    private void Start()
    {
        _position = transform.localPosition;
    }
    public void PrintLetter()
    {
        this.gameObject.transform.DOLocalMoveY(_position.y - _minusY, _pressTime).OnComplete(() =>
        {
            this.gameObject.transform.DOLocalMoveY(_position.y, _pressTime);
        });
        OnSendLetter?.Invoke(_letter);
        Debug.Log(_letter);
    }

    public void DisableLetter()
    {
        this.gameObject.SetActive(false);
    }

    public void EnableLetter()
    {
        this.gameObject.SetActive(true);
    }
}
