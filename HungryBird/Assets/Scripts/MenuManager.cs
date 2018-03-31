using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public static MenuManager instance;
    public RectTransform mainMenu;
    public RectTransform gameMenu;
    public RectTransform firstPanel;
    public RectTransform gameOverMenu;
    public RectTransform pauseMenu;
    public float gameOverPanelDelay = 1f;

    Pause _pause;
    Animator _animator;
    RectTransform currentPanel;
    RectTransform previousPanel;

    private void Start()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);

        _animator = mainMenu.GetComponent<Animator>();
        currentPanel = previousPanel = firstPanel;
        _pause = GetComponent<Pause>();
    }

    // for buttos ClicOn()
    public void ChangePanelTo(RectTransform _rt)
    {
        previousPanel = currentPanel;
        ChangeOperation(_rt);
    }

    // for GameManager to change scene (pause/gameover)
    public void ChangePanelTo(RectTransform _rt, bool disbalePrevious)
    {
        previousPanel = currentPanel;
        previousPanel.gameObject.SetActive(disbalePrevious);
        ChangeOperation(_rt);
    }

    void ChangeOperation(RectTransform _rt)
    {
        currentPanel = _rt;
        currentPanel.gameObject.SetActive(true);
        ChangePanelAnimation();
    }

    public void ToPreviosPanel()
    {
        ChangePanelTo(previousPanel);
    }

    void ChangePanelAnimation()
    {
        _animator = previousPanel.GetComponent<Animator>();
        _animator.SetBool("ShowMenu", false);
        _animator = currentPanel.GetComponent<Animator>();
        _animator.SetBool("ShowMenu", true);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void GameOver()
    {
        Invoke("ChangePanelToGameOver", gameOverPanelDelay);
    }

    void ChangePanelToGameOver()
    {
        ChangePanelTo(gameOverMenu);
        _pause.StopCheckPause();
    }

    public void ChangePanelToPauseMenu()
    {
        ChangePanelTo(pauseMenu);
    }

    public void StartGame()
    {
        ChangePanelTo(gameMenu);
        GameManager.instance.StartGame();
        _pause.CheckPause();
    }

    public void InterruptGame()
    {
        GameManager.instance.InteruptGame();
        ChangePanelTo(mainMenu);
        _pause.StopCheckPause();
    }
}
