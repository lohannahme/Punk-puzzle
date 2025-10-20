using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordHold : MonoBehaviour
{
    [SerializeField] private int _wordHoldIndex;
    public int WordHoldIndex { get => _wordHoldIndex; set => _wordHoldIndex = value; }

    private bool _hasWord = false;
    public bool HasWord { get => _hasWord; set => _hasWord = value; }

}
