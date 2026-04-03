using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RythmMinigame : MonoBehaviour
{
    [SerializeField] private GameObject _PhoneLight;
    [SerializeField] private GameObject[] _faces;
    [SerializeField] private Color _facePressColor;

    [SerializeField] private DrawPaperController _paper;
    private Color _faceNormalColor;


    private bool _isInMinigame = false;
    private int _level = 0;
    private bool _canPress = false;
    void Start()
    {
        _isInMinigame = true;
        PhoneLighter();
        StartMinigame();
        _faceNormalColor = _faces[2].GetComponent<SpriteRenderer>().color;
    }

    private void OnEnable()
    {
        SoundManager.PlayLoop(SoundType.TIKTOK);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            PressFaceColor();
        }

        if (Input.GetMouseButtonUp(0))
        {
            ReleaseFaceColor();
        }
    }

    private void PhoneLighter()
    {
        _PhoneLight.GetComponent<SpriteRenderer>().DOFade(.2f, 2).SetEase(Ease.InOutSine).OnComplete(() =>
        {
            _PhoneLight.GetComponent<SpriteRenderer>().DOFade(1f, 2).SetEase(Ease.OutBounce).SetDelay(1).OnComplete(() => {
                if (_isInMinigame)
                {
                    PhoneLighter();
                }
                else
                {
                    _PhoneLight.GetComponent<SpriteRenderer>().DOFade(0f, 1);
                }

                });
        });
    }

    private void PressFaceColor()
    {
        if (_isInMinigame)
        {

            _faces[2].GetComponent<SpriteRenderer>().DOColor(_facePressColor, .1f);
            if (_canPress == true)
            {
                StartMinigame();
                SoundManager.PlaySound(SoundType.RIGHTPOEM);
                _level += 1;
            }
        }
    }

    private void ReleaseFaceColor()
    {
        if (_isInMinigame)
        {
            _faces[2].GetComponent<SpriteRenderer>().DOColor(_faceNormalColor, .1f);
            if (_level < 3)
            {
                _faces[2].GetComponent<SpriteRenderer>().DOFade(.5f, .1f);
            }
            else
            {
                _faces[2].GetComponent<SpriteRenderer>().DOFade(1f, .1f);
            }
        }
    }

    private void StartMinigame()
    {
        for (int i = 0; i < _faces.Length; i++)
        {
            _faces[i].GetComponent<SpriteRenderer>().DOFade(.5f, .2f);
        }

        if (_level < 2)
        {
            _canPress = false;
            StartCoroutine(Minigame());
        }
        else
        {
            FinishMinigame();
        }

    }

    private IEnumerator Minigame()
    {
        yield return new WaitForSeconds(2);
        _faces[0].GetComponent<SpriteRenderer>().DOFade(1, .5f).OnComplete(() =>
        {
            _faces[0].GetComponent<SpriteRenderer>().DOFade(.5f, .5f);
        });
        yield return new WaitForSeconds(2);
        _faces[1].GetComponent<SpriteRenderer>().DOFade(1, .5f).OnComplete(() =>
        {
            _faces[1].GetComponent<SpriteRenderer>().DOFade(.5f, .5f);
        });
        yield return new WaitForSeconds(2);
        _canPress = true; 
        _faces[2].GetComponent<SpriteRenderer>().DOFade(1, .5f).OnComplete(() =>
        {
        });
    }

    private void FinishMinigame()
    {
        SoundManager.StopLoop();
        for (int i = 0; i < _faces.Length; i++)
        {
            _faces[i].GetComponent<SpriteRenderer>().DOFade(1, .2f);
        }
        _isInMinigame = false;
        _paper.EnableDrawing();
    }
}
