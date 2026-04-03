using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UICursorChange : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    [SerializeField] private Texture2D _cursorNormalTexture;
    [SerializeField] private Texture2D _cursorClickTexture;

    private Vector2 _cursorNormalHotspot;
    private Vector2 _cursorClickHotspot;

    private void Start()
    {
        _cursorNormalHotspot = new Vector2(_cursorNormalTexture.width / 2, _cursorNormalTexture.height / 2);
        Cursor.SetCursor(_cursorNormalTexture, _cursorNormalHotspot, CursorMode.Auto);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _cursorClickHotspot = new Vector2(_cursorClickTexture.width / 2, _cursorClickTexture.height / 2);
        Cursor.SetCursor(_cursorClickTexture, _cursorClickHotspot, CursorMode.Auto);
        Debug.Log("enter");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _cursorNormalHotspot = new Vector2(_cursorNormalTexture.width / 2, _cursorNormalTexture.height / 2);
        Cursor.SetCursor(_cursorNormalTexture, _cursorNormalHotspot, CursorMode.Auto);
        Debug.Log("exit");
    }

 
}
