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
    [SerializeField] private Collider2D _pcCollider;

    private string _mainWord = "relatorio";
    private bool _canDigit = true;

    void Start()
    {
        SetUpWord();
    }

    private void OnEnable()
    {
        LetterButton.OnSendLetter += UpdateWord;
        EraseButton.OnEraseLetter += EraseLetter;
        EnterButton.OnCheckWord += CheckWord;
    }

    private void OnDisable()
    {
        LetterButton.OnSendLetter -= UpdateWord;
        EraseButton.OnEraseLetter -= EraseLetter;
        EnterButton.OnCheckWord -= CheckWord;
    }

    private void SetUpWord()
    {
        _placeholderWord.text = _mainWord;
        _realWord.text = "";
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
            if (_canDigit)
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

    private void CheckWord()
    {
        if(_mainWord == _realWord.text)
        {
            Debug.Log("CERTO");
            _canDigit = false;
            _pcCollider.enabled = true;
        }
        else
        {
            _realWord.text = "";
            _placeholderWord.gameObject.SetActive(true);
        }
    }
}
