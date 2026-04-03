using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawPaperController : CursorChange
{
    [SerializeField] private SpriteRenderer _targetRenderer;
    [SerializeField] private Texture2D _brushTexture;

    private Texture2D _maskTexture;
    private Color32[] _sourcePixels;

    private int _totalPixels;
    private int _revealedPixels;

    private bool _isInDrawingArea = false;

    [SerializeField]private bool _canDraw = false;
    [SerializeField]private bool _isEnd = false;

    public static Action ShowNextSceneButton;
    public static Action<bool> IsDrawing;

    private bool _isMusicLoop = false;

    void Start()
    {
        CloneOriginalTexture();
    }

    void Update()
    {
        if (_canDraw & _isInDrawingArea)
        {
            CleanMethod();
        }
    }

    protected override void MouseEnterVirtual()
    {
        if (_canDraw)
        {
            base.MouseEnterVirtual();
            _isInDrawingArea = true;
        }
    }

    protected override void MouseExitVirtual()
    {
        if (_canDraw)
        {
            base.MouseExitVirtual();
            _isInDrawingArea = false;
            SoundManager.StopLoop();
            IsDrawing?.Invoke(false);
            _isMusicLoop = false;
        }
    }

    public void EnableDrawing()
    {
        _canDraw = true;

    }

    void Reveal(Vector2 pixelUV)
    {
        int brushWidth = Mathf.RoundToInt(_brushTexture.width);
        int brushHeight = Mathf.RoundToInt(_brushTexture.height);


        Color32[] brushPixels = _brushTexture.GetPixels32();

        for (int x = 0; x < brushWidth; x++)
        {
            for (int y = 0; y < brushHeight; y++)
            {
                int px = (int)(pixelUV.x + x - brushWidth / 1.2);
                int py = (int)(pixelUV.y + y - brushHeight / 1.2);

                if (px < 0 || py < 0 || px >= _maskTexture.width || py >= _maskTexture.height)
                    continue;

                Color32 brushPixel = brushPixels[y * brushWidth + x];
                if (brushPixel.a > 0)
                {
                    int index = py * _maskTexture.width + px;
                    Color32 current = _maskTexture.GetPixel(px, py);

                    // revela apenas se ainda estiver invisível
                    if (current.a == 0)
                    {
                        Color32 source = _sourcePixels[index];
                        _maskTexture.SetPixel(px, py, source);
                        _revealedPixels++;
                    }
                }
            }
        }

        _maskTexture.Apply();
    }

    // ===================== SETUP =====================
    private void CloneOriginalTexture()
    {
        Texture2D original = _targetRenderer.sprite.texture;

        _sourcePixels = original.GetPixels32();
        _totalPixels = _sourcePixels.Length;
        _revealedPixels = 0;

        _maskTexture = new Texture2D(
            original.width,
            original.height,
            TextureFormat.RGBA32,
            false
        );

        // começa tudo 100% transparente (sem cor residual)
        Color32[] transparentPixels = new Color32[_totalPixels];
        for (int i = 0; i < transparentPixels.Length; i++)
        {
            transparentPixels[i] = new Color32(0, 0, 0, 0);
        }

        _maskTexture.SetPixels32(transparentPixels);
        _maskTexture.Apply();

        var originalSprite = _targetRenderer.sprite;
        _targetRenderer.sprite = Sprite.Create(
            _maskTexture,
            originalSprite.rect,
            new Vector2(0.5f, 0.5f),
            originalSprite.pixelsPerUnit
        );
    }

    // ===================== INPUT =====================
    private void CleanMethod()
    {
        if (Input.GetMouseButton(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 pixelUV = WorldToPixelCoordinates(mousePos, _targetRenderer);

            Reveal(pixelUV);

            GetRevealedPercent();
            IsDrawing?.Invoke(true);
            if (_isMusicLoop == false)
            {
                SoundManager.PlayLoop(SoundType.PENCIL);
                _isMusicLoop = true;
            }
        }

        //if (Input.GetMouseButtonDown(0))
        //{
        //    SoundManager.PlayLoop(SoundType.PENCIL);
        //}

        if (Input.GetMouseButtonUp(0))
        {
            SoundManager.StopLoop();
            _isMusicLoop = false;
            IsDrawing?.Invoke(false);
        }
    }

    Vector2 WorldToPixelCoordinates(Vector2 worldPos, SpriteRenderer renderer)
    {
        Vector2 localPos = renderer.transform.InverseTransformPoint(worldPos);
        Vector2 spritePivot = renderer.sprite.pivot;

        float pixelsPerUnit = renderer.sprite.pixelsPerUnit;
        Vector2 pixelCoord = spritePivot + localPos * pixelsPerUnit;

        return pixelCoord;
    }

    // ===================== PROGRESS =====================
    public float GetRevealedPercent()
    {
        float percent = (float)_revealedPixels / _totalPixels;
        Debug.Log($"Pixels revelados: {percent}");
        Debug.Log($"total pixels{_totalPixels}");
        if(percent >3 & _isEnd == false)
        {
            ShowNextSceneButton?.Invoke();
        }
        return percent;
    }

}
