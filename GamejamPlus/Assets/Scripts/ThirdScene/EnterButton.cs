using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnterButton : MonoBehaviour
{
    public static Action OnCheckWord;

    public void ClickButton()
    {
        OnCheckWord?.Invoke();
    }
}
