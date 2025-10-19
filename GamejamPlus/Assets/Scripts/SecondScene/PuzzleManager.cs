using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    [SerializeField] private GameObject _puzzleHoldPosition;
    [SerializeField] private GameObject _puzzlePiecesPosition;
    [SerializeField] private PuzzleConfigSO[] _puzzleConfigs;

    private int _currentRightPieces = 0;
    private int _totalPieces = 0;
    void Start()
    {
        SetUpConfig();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SetUpConfig()
    {
        //colocar game manager do dia

    }
}

