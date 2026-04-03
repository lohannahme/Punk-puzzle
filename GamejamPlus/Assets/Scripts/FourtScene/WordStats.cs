using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordStats : MonoBehaviour
{
    [SerializeField] private int _pieceIndex;

    private GameObject _paperBall;
    private GameObject _pieceShadow;
    private GameObject _pieceImage;

    private WordHold _hold = null;

    private float _maxScaleAdditive = 1.05f;
    private float _timeToScale = 0.15f;
    private float _timeToMove = 0.15f;

    private Vector3 _initialScale;
    private Vector3 _initialPosition;
    private Quaternion _initialRotation;
    private Vector3 _shadowPosition = new Vector3(.31f, -.31f, 0f);

    private bool _hasPiece = false;
    private bool _isInTable = true;

    public static Action OnAddWords;
    public static Action<int> OnPositionWords;
    public static Action OnPositionWordsExit;
    // Start is called before the first frame update
    void Start()
    {
        _initialScale = transform.localScale;
        _initialPosition = transform.localPosition;
        _initialRotation = transform.rotation;
        _pieceImage = gameObject.transform.GetChild(0).gameObject;
        _pieceShadow = _pieceImage.transform.GetChild(0).gameObject;
        _paperBall = gameObject.transform.GetChild(1).gameObject;
    }

    public void OnClick()
    {
        gameObject.transform.DOScale(_initialScale * _maxScaleAdditive, _timeToScale).SetEase(Ease.InOutSine);
        gameObject.transform.DOLocalRotate(Vector3.zero, _timeToScale);
        _pieceShadow.transform.DOLocalMove(_shadowPosition, _timeToScale).OnComplete(() =>
        {
            _hasPiece = false;
        });

    }

    public void OnRelease()
    {
        gameObject.transform.DOScale(_initialScale, _timeToScale).SetEase(Ease.InOutSine);
        gameObject.transform.DOLocalRotate(_initialRotation.eulerAngles, _timeToScale);
        _pieceShadow.transform.DOLocalMove(Vector3.zero, _timeToScale).OnComplete(() =>
        {
            _hasPiece = true;
        });
    }

    public void ReturnToInitialPosition()
    {
        gameObject.transform.DOLocalMove(_initialPosition, 1);
        StartCoroutine(DelayToReturn());
    }

    IEnumerator DelayToReturn()
    {
        yield return new WaitForSeconds(.5f);
        OnRelease();
    }

    public void VerifyIfThePieceIsRight()
    {
        if (_hold != null)
        {
            if (_hold.WordHoldIndex == _pieceIndex)
            {
                SoundManager.PlaySound(SoundType.RIGHTPOEM);
                _hasPiece = false;
                Debug.Log("certo");
                OnAddWords?.Invoke();

                this.gameObject.SetActive(false);
                _isInTable = false;
            }
            else
            {
                if (_pieceIndex == -1)
                {
                    SoundManager.PlaySound(SoundType.WRONGPOEM);
                    gameObject.GetComponent<Collider2D>().enabled = false; ;
                    _pieceImage.transform.DOScale(0, .5f).OnComplete(() =>
                    {
                        _paperBall.transform.DOScale(.3f, .5f).OnComplete(() =>
                        {
                            StartCoroutine(DelayDisable());
                            _isInTable = false;
                        });
                    });
                    //OnAddWords?.Invoke();
                }
                else
                {
                    ReturnToInitialPosition();
                }
            }

        }
        else
        {
            Debug.Log("has not a piece right");
        }
    }

    public void DiscardPapers()
    {
        if (_isInTable)
        {
            gameObject.GetComponent<Collider2D>().enabled = false; ;
            _pieceImage.transform.DOScale(0, .5f).OnComplete(() =>
            {
                _paperBall.transform.DOScale(.3f, .5f).OnComplete(() =>
                {
                    StartCoroutine(DelayDisable());
                    _isInTable = false;
                });
            });
        }
    }

    IEnumerator DelayDisable()
    {
        gameObject.GetComponent<Rigidbody2D>().gravityScale = 1;
        yield return new WaitForSeconds(3f);
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<WordHold>())
        {
            if(collision.gameObject.GetComponent<WordHold>().HasWord == false & _hasPiece == false)
            {
                _hold = collision.gameObject.GetComponent<WordHold>();
                Debug.Log("enter right hold");
                _pieceImage.transform.DOMove(_hold.gameObject.transform.position, _timeToMove);
                _pieceImage.transform.DOScale(_initialScale, _timeToMove).SetEase(Ease.InOutSine);
                OnRelease();
                _pieceImage.transform.SetParent(_hold.gameObject.transform);
                OnPositionWords?.Invoke(_hold.WordHoldIndex);
                _hold.HasWord = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<WordHold>())
        {
            if(_hasPiece == true & _hold != null)
            {
                _hold.HasWord = false;
                _hold = null;
                _pieceImage.transform.DOLocalMove(Vector3.zero, _timeToScale);
                _pieceImage.transform.DOLocalRotate(Vector3.zero, _timeToScale);
                _pieceImage.transform.SetParent(this.gameObject.transform);
                Debug.Log("exit right hold");
                OnClick();
            }
        }
    }
}
