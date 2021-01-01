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

    void Update()
    {
        //Debug.Log(StateName(GameState.currentState));

        if(GameState.currentState == GameState.LevelStatus.levelFailed && !gameOverUI.activeSelf)
            GameOver();

        if(GameState.currentState == GameState.LevelStatus.levelComplete && !victoryUI.activeSelf)
            Victory();

        if(GameState.currentState == GameState.LevelStatus.levelInProgress)
        {
            if(Input.GetKeyDown(KeyCode.Escape) && !GameState.paused)
                Pause();
            else if(Input.GetKeyDown(KeyCode.Escape))
                UnPause();
        }
    }

    string StateName(GameState.LevelStatus _state)
    {
        if(_state == GameState.LevelStatus.levelInProgress)
            return "Level in Progress";
        else if (_state == GameState.LevelStatus.levelComplete)
            return "Level Complete";
        else
            return "Level Failed";
    }

    public void Pause()
    {
        enableOne(pauseUI);
        GameState.paused = true;
        Time.timeScale = 0f;
    }

    public void UnPause()
    {
        enableOne(gameUI);
        GameState.paused = false;
        Time.timeScale = 1f;
    }

    void Victory()
    {
        enableOne(victoryUI);
        GameState.paused = true;
        Time.timeScale = 0f;
    }

    void GameOver()
    {
        enableOne(gameOverUI);
        //GameState.paused = true;
        Time.timeScale = 0f;
    }

    public void QuitToMainMenu()
    {
        Time.timeScale = 1f;
        GameState.paused = false;
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
