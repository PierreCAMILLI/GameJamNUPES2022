using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private RectTransform _titleScreen;
    [SerializeField] private RectTransform _tutorialScreen;
    [SerializeField] private LoadingBarUI _loadingBar;

    [Space]
    [SerializeField] private UnityEngine.Object _gameScene;

    private RectTransform _currentlyDisplayedScreen;

    private void Awake()
    {
        DisableAllScreens();
        DisplayScreen(_titleScreen);
    }

    private void Reset()
    {
        FindComponents_Editor();
    }

    public void OnPlayButtonPressed()
    {
        AsyncOperation operation = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(
            _gameScene.name,
            UnityEngine.SceneManagement.LoadSceneMode.Single);
        _loadingBar.SetOperation(operation);
    }

    public void OnTutorialButtonPressed()
    {
        DisplayScreen(_tutorialScreen);
    }

    public void OnQuitButtonPressed()
    {
        Application.Quit();
    }

    public void OnBackButtonPressed()
    {
        DisplayScreen(_titleScreen);
    }

    private void DisplayScreen(RectTransform rectTransform)
    {
        _currentlyDisplayedScreen?.gameObject.SetActive(false);
        _currentlyDisplayedScreen = rectTransform;
        _currentlyDisplayedScreen?.gameObject.SetActive(true);
    }

    private void DisableAllScreens()
    {
        _titleScreen?.gameObject.SetActive(false);
        _tutorialScreen?.gameObject.SetActive(false);
    }

    [ContextMenu("Find Components")]
    private void FindComponents_Editor()
    {
        _loadingBar = FindObjectOfType<LoadingBarUI>();
    }
}
