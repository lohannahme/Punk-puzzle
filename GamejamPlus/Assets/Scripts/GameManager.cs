using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject[] _GameSceneOne;
    [SerializeField] private GameObject[] _GameSceneTwo;
    [SerializeField] private GameObject[] _GameSceneThree;
    [SerializeField] private GameObject[] _GameSceneFour;
    [SerializeField] private GameObject[] _GameSceneFive;

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
    }

    private void OnDisable()
    {
        GameSceneConfig.OnGetWord -= ChangeGameScene;
        TableMinigameConfig.OnFinishGame -= ChangeGameScene;
    }

    private void StartGame()
    {
        _currentGameSceneObject = _GameSceneOne[0];
        EnterScene(_currentGameSceneObject);
    }

    private void EnterScene(GameObject scenePrefab)
    {
        scenePrefab.transform.localPosition = new Vector3(18f, 0f, 0f);
        scenePrefab.SetActive(true);
        scenePrefab.transform.DOLocalMoveX(0f, 1).SetEase(Ease.OutCubic);
    }

    private void ExitScene(GameObject scenePrefab)
    {
        scenePrefab.transform.DOLocalMoveX(-18f, 1).SetEase(Ease.InCubic).OnComplete(() =>
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
        yield return new WaitForSeconds(1);
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
