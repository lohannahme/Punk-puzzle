using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PieceStats : MonoBehaviour
{
    [SerializeField] private int _pieceIndex;

    private GameObject _pieceShadow;
    private GameObject _pieceImage;

    private PieceHold _hold = null;

    private float _maxScaleAdditive = 1.1f;
    private float _timeToScale = 0.15f;
    private float _timeToMove = 0.3f;

    private Vector3 _initialScale;
    private Vector3 _shadowPosition = new Vector3(.2f, -.2f, 0f);

    private bool _hasPiece = true;

    public static Action OnCorrectPiecePlaced;

    void Start()
    {
        _initialScale = transform.localScale;
        _pieceImage = gameObject.transform.GetChild(0).gameObject;
        _pieceShadow = _pieceImage.transform.GetChild(0).gameObject;
    }

    public void OnClick()
    {
        gameObject.transform.DOScale(_initialScale * _maxScaleAdditive, _timeToScale).SetEase(Ease.InOutSine);
        _pieceShadow.transform.DOLocalMove(_shadowPosition, _timeToScale);
    }

    public void OnRelease()
    {
        gameObject.transform.DOScale(_initialScale, _timeToScale).SetEase(Ease.InOutSine);
        _pieceShadow.transform.DOLocalMove(Vector3.zero, _timeToScale);
    }

    public void VerifyIfThePieceIsRight()
    {
        if(_hold != null)
        {
            _hasPiece = false;
            _hold.HoldHasAPiece();
            OnCorrectPiecePlaced?.Invoke();
            this.gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("has not a piece right");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<PieceHold>() && collision.gameObject.GetComponent<PieceHold>().PieceHoldIndex == _pieceIndex)
        {
            _hold = collision.gameObject.GetComponent<PieceHold>();
            Debug.Log("enter right hold");
            _pieceImage.transform.DOMove(_hold.gameObject.transform.position, _timeToMove);
            _pieceImage.transform.DOScale(_initialScale, _timeToMove).SetEase(Ease.InOutSine);
            OnRelease(); 
            _pieceImage.transform.SetParent(_hold.gameObject.transform);
            _hold.HasPiece = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PieceHold>() && collision.gameObject.GetComponent<PieceHold>().PieceHoldIndex == _pieceIndex)
        {
            if (_hasPiece)
            {
                _hold.HasPiece = false;
                _hold = null;
                _pieceImage.transform.DOLocalMove(Vector3.zero, _timeToScale);
                _pieceImage.transform.SetParent(this.gameObject.transform);
                Debug.Log("exit right hold");
                OnClick();
            }
        }
    }
}
