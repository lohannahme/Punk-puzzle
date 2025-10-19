using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EraseButton : MonoBehaviour
{
    public static Action OnEraseLetter;

    public void ClickButton()
    {
        OnEraseLetter?.Invoke();
    }
}
