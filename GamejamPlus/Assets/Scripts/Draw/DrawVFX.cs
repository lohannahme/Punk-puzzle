using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawVFX : MonoBehaviour
{
    [SerializeField] private GameObject _particleObject;

    private Vector2 _mousePos;

    private bool _isActive = false;
    // Start is called before the first frame update
    void Start()
    {
        _particleObject.SetActive(false);
    }

    private void OnEnable()
    {
        DrawPaperController.IsDrawing += EnableParticles;
    }

    private void OnDisable()
    {
        DrawPaperController.IsDrawing -= EnableParticles;
    }

    // Update is called once per frame
    void Update()
    {
        if (_isActive)
        {
            _mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            _particleObject.transform.position = _mousePos;
            _particleObject.SetActive(true);
        }
        else
        {
            _particleObject.SetActive(false);
        }
    }

    private void EnableParticles(bool IsDrawing)
    {
        _isActive = IsDrawing;
    }

}
