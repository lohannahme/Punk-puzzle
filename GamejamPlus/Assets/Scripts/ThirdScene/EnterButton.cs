using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterButton : MonoBehaviour
{
    public static Action OnCheckWord;

    private float _pressTime = .1f;

    private float _minusY = .335f;

    private Vector3 _position;


    private void Start()
    {
        _position = transform.localPosition;
    }

    public void ClickButton()
    {
        this.gameObject.transform.DOLocalMoveY(_position.y - _minusY, _pressTime).OnComplete(() =>
        {
            this.gameObject.transform.DOLocalMoveY(_position.y, _pressTime);
        });
        OnCheckWord?.Invoke();
    }
}
