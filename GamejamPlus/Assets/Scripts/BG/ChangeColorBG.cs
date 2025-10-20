using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ChangeColorBG : MonoBehaviour
{
    [SerializeField] private Renderer _bgRenderer;

    [SerializeField] private Color[] _levelColors;

    [SerializeField] private float _timeToChangeColor;

    private int _index = 0;
    private Material _bgColor;
    void Start()
    {
        _bgColor = _bgRenderer.GetComponent<Renderer>().material;
        _bgColor.color = _levelColors[0];
    }

    private void OnEnable()
    {
        GameSceneConfig.OnGetWord += ChangeLevelColor;
    }

    private void OnDisable()
    {
        GameSceneConfig.OnGetWord -= ChangeLevelColor;
    }

    private void ChangeLevelColor()
    {
        _index += 1;
        if(_index > _levelColors.Length - 1)
        {
            _index = 0;
        }
        _bgColor.DOColor(_levelColors[_index], _timeToChangeColor);
    }
}
