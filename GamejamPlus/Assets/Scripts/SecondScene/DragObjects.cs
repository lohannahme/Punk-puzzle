using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragObjects : MonoBehaviour
{
    private Transform _dragging = null;
    private Vector3 _offset;
    private PieceStats _currentPiece = null;

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
        }else if (Input.GetMouseButtonUp(0))
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
}
