using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public static class GameState
{
    public enum LevelStatus { levelFailed, levelComplete, levelInProgress };
    public static LevelStatus currentState = LevelStatus.levelInProgress;
    public static bool paused = false;
}

public class InGameUI : MonoBehaviour
{
    public GameObject gameUI;
    public GameObject pauseUI;
    public GameObject victoryUI;
    public GameObject gameOverUI;

    private void Start()
    {
        PlayerVariables.playerControls.InGame.Pause.performed += cxt => TogglePauseState();
    }

    private void Update()
    {
        if(GameState.currentState == GameState.LevelStatus.levelFailed && !gameOverUI.activeSelf)
            GameOver();

        if(GameState.currentState == GameState.LevelStatus.levelComplete && !victoryUI.activeSelf)
            Victory();
    }

    private void TogglePauseState()
    {
        if(GameState.currentState == GameState.LevelStatus.levelInProgress)
        {
            if(!GameState.paused)
                Pause();
            else
                UnPause();
        }
    }

    public void Pause()
    {
        enableOne(pauseUI);
        Time.timeScale = 0f;
        GameState.paused = true;
    }

    public void UnPause()
    {
        enableOne(gameUI);
        Time.timeScale = 1f;
        GameState.paused = false;
    }

    private void Victory()
    {
        enableOne(victoryUI);
        GameState.paused = true;
    }

    private void GameOver()
    {
        enableOne(gameOverUI);
        GameState.paused = true;
    }

    public void QuitToMainMenu()
    {
        GameState.paused = false;
        Time.timeScale = 1f;
        GameState.currentState = GameState.LevelStatus.levelInProgress;
        SceneManager.LoadScene("Main Menu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    void enableOne(GameObject _object)  
    {
        foreach (Transform child in transform)
            child.gameObject.SetActive(false);

        _object.SetActive(true);
    }
}
