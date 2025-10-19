using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WordManager : MonoBehaviour
{
    [SerializeField] private TextMeshPro _placeholderWord;
    [SerializeField] private TextMeshPro _realWord;
    [Space(2)]
    [SerializeField] private LetterButton[] _letters;

    private string _mainWord = "relatorio";

    void Start()
    {
        SetUpWord();
    }

    private void OnEnable()
    {
        LetterButton.OnSendLetter += UpdateWord;
        EraseButton.OnEraseLetter += EraseLetter;
    }

    private void OnDisable()
    {
        LetterButton.OnSendLetter -= UpdateWord;
        EraseButton.OnEraseLetter -= EraseLetter;
    }

    private void SetUpWord()
    {
        _placeholderWord.text = _mainWord;
        _realWord.text = "";

        for (int i = 0; i < _letters.Length; i++)
        {
            _letters[i].DisableLetter();
        }

        char[] characters = _mainWord.ToCharArray();

        for (int i = 0; i <_letters.Length; i++)
        {
            for(int y = 0; y < characters.Length; y++)
            {
                if (_letters[i].Letter == characters[y])
                {
                    _letters[i].EnableLetter();
                }
            }
        }
    }

    private void EraseLetter()
    {
        string newString = _realWord.text.Remove(_realWord.text.Length - 1);
        _realWord.text = newString;
        if(_realWord.text == "")
        {
            _placeholderWord.gameObject.SetActive(true);
        }
    }

    private void UpdateWord(char charletter)
    {
        _placeholderWord.gameObject.SetActive(false);
        _realWord.text += charletter;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit)
            {
                if (hit.collider.gameObject.GetComponent<LetterButton>())
                {
                    hit.collider.gameObject.GetComponent<LetterButton>().PrintLetter();
                }

                if (hit.collider.gameObject.GetComponent<EraseButton>())
                {
                    hit.collider.gameObject.GetComponent<EraseButton>().ClickButton();
                }

                if (hit.collider.gameObject.GetComponent<EnterButton>())
                {
                    hit.collider.gameObject.GetComponent<EnterButton>().ClickButton();
                }

            }
        }
    }
}
