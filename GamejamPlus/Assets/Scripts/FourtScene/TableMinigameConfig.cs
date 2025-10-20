using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEditor;
using System;

public class TableMinigameConfig : MonoBehaviour
{
    [SerializeField] private GameObject _table;
    [SerializeField] private int _totalWords;

    private float _zoomTableScale = 0.92f;
    private Vector3 _initalTableScale;

    private Transform _dragging = null;
    private Vector3 _offset;
    private WordStats _currentPiece = null;

    private int _currentWords = 0;

    public static Action OnFinishGame;

    private void OnEnable()
    {
        WordStats.OnAddWords += VerifyCorrectWords;
    }

    private void OnDisable()
    {
        WordStats.OnAddWords -= VerifyCorrectWords;
    }

    private void Start()
    {
        _initalTableScale = _table.transform.localScale;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit)
            {
                if (hit.collider.gameObject.CompareTag("Table"))
                {
                    hit.collider.enabled = false;
                    _table.transform.DOScale(_zoomTableScale,1);
                }

                if(hit.collider.gameObject.GetComponent<WordStats>())
                {
                    _currentPiece = hit.collider.gameObject.GetComponent<WordStats>();
                    _dragging = hit.transform;
                    _currentPiece.OnClick();

                    _offset = _dragging.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
                }

            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (_currentPiece)
            {
                _currentPiece.OnRelease();
                _currentPiece.VerifyIfThePieceIsRight();
                _dragging = null;
                _currentPiece = null;
            }
        }

        if (_dragging != null)
        {
            _dragging.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + _offset;
        }
    }

    private void VerifyCorrectWords()
    {
        _currentWords += 1;
        if(_currentWords == _totalWords)
        {
            _table.transform.DOScale(_initalTableScale, .5f).OnComplete(() =>
            {
                OnFinishGame?.Invoke();
            });
        }
    }
}
