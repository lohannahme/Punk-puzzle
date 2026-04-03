using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorChange : MonoBehaviour
{
    [SerializeField] protected Texture2D _cursorNormalTexture;
    [SerializeField] protected Texture2D _cursorClickTexture;

    protected Vector2 _cursorNormalHotspot;
    protected Vector2 _cursorClickHotspot;
    void Start()
    {
        _cursorNormalHotspot = new Vector2(_cursorNormalTexture.width / 2, _cursorNormalTexture.height / 2);
        Cursor.SetCursor(_cursorNormalTexture, _cursorNormalHotspot, CursorMode.Auto);
    }

    private void OnMouseEnter()
    {
        MouseEnterVirtual();
    }

    private void OnMouseExit()
    {
        MouseExitVirtual();
    }

    protected virtual void MouseEnterVirtual()
    {
        _cursorClickHotspot = new Vector2(_cursorClickTexture.width / 2, _cursorClickTexture.height / 2);
        Cursor.SetCursor(_cursorClickTexture, _cursorClickHotspot, CursorMode.Auto);
        Debug.Log("mouse enter");
    }

    protected virtual void MouseExitVirtual()
    {
        _cursorNormalHotspot = new Vector2(_cursorNormalTexture.width / 2, _cursorNormalTexture.height / 2);
        Cursor.SetCursor(_cursorNormalTexture, _cursorNormalHotspot, CursorMode.Auto);
        Debug.Log("mouse exit");
    }
}
