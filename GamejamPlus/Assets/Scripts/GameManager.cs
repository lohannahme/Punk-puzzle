using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject[] _GameSceneOne;
    [SerializeField] private GameObject[] _GameSceneTwo;
    [SerializeField] private GameObject[] _GameSceneThree;
    [SerializeField] private GameObject[] _GameSceneFour;
    [SerializeField] private GameObject[] _GameSceneFive;

    [SerializeField] private GameObject _transitionPrefab;
    [SerializeField] private GameObject _nextSceneButtonPrefab;

    private int _gameScene = 0;
    private int _totalScenes = 5;
    private int _gameDay = 0;

    private GameObject _currentGameSceneObject;

    void Start()
    {
        StartGame();
    }

    private void OnEnable()
    {
        GameSceneConfig.OnGetWord += ChangeGameScene;
        TableMinigameConfig.OnFinishGame += ChangeGameScene;
        DrawPaperController.ShowNextSceneButton += ShowNextSceneButton;
    }

    private void OnDisable()
    {
        GameSceneConfig.OnGetWord -= ChangeGameScene;
        TableMinigameConfig.OnFinishGame -= ChangeGameScene;
        DrawPaperController.ShowNextSceneButton -= ShowNextSceneButton;
    }

    private void StartGame()
    {
        _currentGameSceneObject = _GameSceneOne[0];
        _transitionPrefab.SetActive(true);
        EnterScene(_currentGameSceneObject);
    }

    //private void EnterScene(GameObject scenePrefab)
    //{
    //    scenePrefab.transform.localPosition = new Vector3(18f, 0f, 0f);
    //    scenePrefab.SetActive(true);
    //    scenePrefab.transform.DOLocalMoveX(0f, 1).SetEase(Ease.OutCubic);
    //}

    private void EnterScene(GameObject scenePrefab)
    {
        scenePrefab.SetActive(true);
        HideNextSceneButton();
        _transitionPrefab.GetComponent<Image>().DOFade(0, 1f).OnComplete(() =>
        {
             _transitionPrefab.SetActive(false);
        });

    }

    private void ExitScene(GameObject scenePrefab)
    {
        _transitionPrefab.SetActive(true);
        _transitionPrefab.GetComponent<Image>().DOFade(1, .5f).OnComplete(() =>
        {
            scenePrefab.SetActive(false);
        });
    }

    private void ChangeGameScene()
    {
        ExitScene(_currentGameSceneObject);
        StartCoroutine(DelayToChangeScene());

    }

    private IEnumerator DelayToChangeScene()
    {
        yield return new WaitForSeconds(1.6f);
        _gameScene += 1;
        if (_gameScene > _totalScenes - 1)
        {
            _gameScene = 0;
            _gameDay += 1;
        }
        switch (_gameScene)
        {
            case 0:
                _currentGameSceneObject = _GameSceneOne[_gameDay];
                break;
            case 1:
                _currentGameSceneObject = _GameSceneTwo[_gameDay];
                break;
            case 2:
                _currentGameSceneObject = _GameSceneThree[_gameDay];
                break;
            case 3:
                _currentGameSceneObject = _GameSceneFour[_gameDay];
                break;
            case 4:
                _currentGameSceneObject = _GameSceneFive[_gameDay];
                break;
            default:
                break;

        }

        EnterScene(_currentGameSceneObject);
    }

    private void ShowNextSceneButton()
    {
        _nextSceneButtonPrefab.GetComponent<BoxCollider2D>().enabled = true;
        _nextSceneButtonPrefab.SetActive(true);
        _nextSceneButtonPrefab.transform.DOMoveY(-4.42f, .5f);
    }

    private void HideNextSceneButton()
    {
        _nextSceneButtonPrefab.SetActive(false);
        _nextSceneButtonPrefab.transform.position = new Vector3(6f, -5.65f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit)
            {

                if (hit.collider.gameObject.CompareTag("Word"))
                {
                    Debug.Log("new scene");
                    hit.collider.enabled = false;
                    ChangeGameScene();
                }
            }
        }
    }
}
