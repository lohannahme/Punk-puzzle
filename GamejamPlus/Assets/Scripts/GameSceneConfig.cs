using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class GameSceneConfig : MonoBehaviour
{
    [SerializeField] private GameObject _gameSceneWord;

    public static Action OnGetWord;

    private Vector3 _initialScale;
    private float _showWord = .5f;
    void Start()
    {
        _initialScale = _gameSceneWord.transform.localScale;
        _gameSceneWord.transform.localScale = Vector3.zero;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit)
            {
                if(hit.collider.gameObject.CompareTag("WordGen"))
                {
                    ShowWord();
                    hit.collider.enabled = false;
                }

                if(hit.collider.gameObject.CompareTag("Word"))
                {
                    Debug.Log("new scene");
                    hit.collider.enabled = false;
                    OnGetWord?.Invoke();
                }
            }
        }
    }

    public void ShowWord()
    {
        _gameSceneWord.transform.DOScale(_initialScale, _showWord);
    }
}
