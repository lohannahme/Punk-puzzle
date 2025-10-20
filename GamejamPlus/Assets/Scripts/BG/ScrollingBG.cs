using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBG : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Renderer _bgRenderer;


    void Update()
    {
        _bgRenderer.material.mainTextureOffset += new Vector2(_speed * Time.deltaTime, _speed * Time.deltaTime);
    }
}
