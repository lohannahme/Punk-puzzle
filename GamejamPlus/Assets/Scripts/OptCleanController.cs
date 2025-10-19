using UnityEngine;

public class ScratchController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _targetRenderer;
    [SerializeField] private Texture2D _brushTexture;
    private Texture2D _maskTexture;
    private Color32[] _originalPixels;
    private int _totalPixels;
    private int _clearedPixels;

    void Start()
    {
        CloneOriginalTexture();
    }

    void Update()
    {
        CleanMethod();
    }

    void Erase(Vector2 pixelUV)
    {
        int brushWidth = _brushTexture.width;
        int brushHeight = _brushTexture.height;

        Color32[] brushPixels = _brushTexture.GetPixels32();

        for (int x = 0; x < brushWidth; x++)
        {
            for (int y = 0; y < brushHeight; y++)
            {
                int px = (int)(pixelUV.x + x - brushWidth / 2);
                int py = (int)(pixelUV.y + y - brushHeight / 2);

                if (px < 0 || py < 0 || px >= _maskTexture.width || py >= _maskTexture.height)
                    continue;

                Color32 brushPixel = brushPixels[y * brushWidth + x];
                if (brushPixel.a > 0)
                {
                    int index = py * _maskTexture.width + px;
                    Color32 current = _maskTexture.GetPixel(px, py);

                    if (current.a > 0)
                    {
                        current.a = 0;
                        _maskTexture.SetPixel(px, py, current);
                        _clearedPixels++;
                    }
                }
            }
        }

        _maskTexture.Apply();
    }

    private void CloneOriginalTexture()
    {
        Texture2D original = _targetRenderer.sprite.texture;
        _maskTexture = new Texture2D(original.width, original.height, TextureFormat.RGBA32, false);
        _maskTexture.SetPixels32(original.GetPixels32());
        _maskTexture.Apply();

        var originalSprite = _targetRenderer.sprite;
        _targetRenderer.sprite = Sprite.Create(
            _maskTexture,
            originalSprite.rect,
            new Vector2(0.5f, 0.5f),
            originalSprite.pixelsPerUnit
        );

        _originalPixels = _maskTexture.GetPixels32();
        _totalPixels = _originalPixels.Length;
        _clearedPixels = 0;
    }

    private void CleanMethod()
    {
        if (Input.GetMouseButton(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 pixelUV = WorldToPixelCoordinates(mousePos, _targetRenderer);

            Erase(pixelUV);
            VerifyIfItsClean();
        }
    }

    Vector2 WorldToPixelCoordinates(Vector2 worldPos, SpriteRenderer renderer)
    {
        Vector2 localPos = renderer.transform.InverseTransformPoint(worldPos);
        Vector2 spriteSize = renderer.sprite.rect.size;
        Vector2 spritePivot = renderer.sprite.pivot;

        float pixelsPerUnit = renderer.sprite.pixelsPerUnit;
        Vector2 pixelCoord = spritePivot + localPos * pixelsPerUnit;

        return pixelCoord;
    }

    public float GetClearedPercent()
    {
        Debug.Log($"pixels porc {(float)_clearedPixels / _totalPixels}");
        return (float)_clearedPixels / _totalPixels;
    }

    private void VerifyIfItsClean()
    {
        if(GetClearedPercent()> 0.9f)
        {
            _targetRenderer.gameObject.SetActive(false);
        }
    }
}
