using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Puzzle Config", menuName = "ScriptableObjects/PuzzleSO", order = 1)]
public class PuzzleConfigSO : ScriptableObject
{
    public int pieceCount;
    public GameObject busPrefab;
    public GameObject puzzlePiecesPrefab;
}
