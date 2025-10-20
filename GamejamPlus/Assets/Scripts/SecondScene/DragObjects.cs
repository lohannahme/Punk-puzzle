using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragObjects : MonoBehaviour
{
    [SerializeField] private int _totalPieces;
    [SerializeField] private Collider2D _busCollider;

    private Transform _dragging = null;
    private Vector3 _offset;
    private PieceStats _currentPiece = null;
    private int _currentPieces = 0;


    private void OnEnable()
    {
        PieceStats.OnCorrectPiecePlaced += VerifyPieces;
    }

    private void OnDisable()
    {
        PieceStats.OnCorrectPiecePlaced -= VerifyPieces;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit & hit.collider.gameObject.GetComponent<PieceStats>())
            {
                _currentPiece = hit.collider.gameObject.GetComponent<PieceStats>();
                _dragging = hit.transform;
                _currentPiece.OnClick();

                _offset = _dragging.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
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

    private void VerifyPieces()
    {
        _currentPieces += 1;
        if(_currentPieces == _totalPieces)
        {
            _busCollider.enabled = true;
        }
    }
}
