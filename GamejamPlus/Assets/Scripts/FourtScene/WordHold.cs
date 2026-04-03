using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordHold : MonoBehaviour
{
    [SerializeField] private int _wordHoldIndex;

    private BoxCollider2D _wordHoldCollider;
    public int WordHoldIndex { get => _wordHoldIndex; set => _wordHoldIndex = value; }

    private bool _hasWord = false;
    public bool HasWord { get => _hasWord; set => _hasWord = value; }

    private void Start()
    {
        GetComponents();
    }

    private void OnEnable()
    {
        //AddListeners();
    }

    private void GetComponents()
    {
        Debug.Log("GET COMPONENTS");
        _wordHoldCollider = this.gameObject.GetComponent<BoxCollider2D>();
    }

    private void AddListeners()
    {
        WordStats.OnPositionWords += DisableCollider;
        WordStats.OnPositionWordsExit += EnableCollider;
    }

    private void DisableCollider(int index)
    {
        if(_wordHoldIndex != index)
        {
            _wordHoldCollider.enabled = false;
        }
    }

    private void EnableCollider()
    {
        _wordHoldCollider.enabled = true;  
    }

}
