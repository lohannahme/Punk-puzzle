using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterButton : MonoBehaviour
{
    [SerializeField] private char _letter;
    public char Letter { get => _letter; set => _letter = value; }

    public static Action<char> OnSendLetter;

    public void PrintLetter()
    {
        OnSendLetter?.Invoke(_letter);
        Debug.Log(_letter);
    }

    public void DisableLetter()
    {
        this.gameObject.SetActive(false);
    }

    public void EnableLetter()
    {
        this.gameObject.SetActive(true);
    }
}
