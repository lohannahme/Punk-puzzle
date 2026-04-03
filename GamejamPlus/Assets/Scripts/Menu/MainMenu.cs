using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject _transitionPrefab;
    [SerializeField] private GameObject _currentScene;

    public void PlayGame()
    {
        _transitionPrefab.GetComponent<Image>().DOFade(1, .5f).OnComplete(() =>
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        });

    }

    public void ChangeScene(GameObject newScene)
    {
        _transitionPrefab.GetComponent<Image>().DOFade(1, .5f).OnComplete(() =>
        {
            _currentScene.SetActive(false);
            newScene.SetActive(true);
            _currentScene = newScene;
            _transitionPrefab.GetComponent<Image>().DOFade(0, .5f);
        });

        
    }
}
