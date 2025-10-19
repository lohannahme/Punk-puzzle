using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceHold : MonoBehaviour
{
    [SerializeField] private int _pieceHoldIndex;
    public int PieceHoldIndex { get => _pieceHoldIndex; set => _pieceHoldIndex = value; }

    private bool _hasPiece = false;
    public bool HasPiece { get => _hasPiece; set => _hasPiece = value; }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HoldHasAPiece()
    {
        Debug.Log("HAS A PIECE");
    }
}
