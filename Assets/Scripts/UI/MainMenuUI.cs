using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    public GameObject mainMenuUI;
    public GameObject controlsUI;
    public GameObject settingsUI;

    public void OpenControlsUI()
    {
        enableOne(controlsUI);
    }

    public void OpenMainMenuUI()
    {
        enableOne(mainMenuUI);
    }

    public void OpenSettingsUI()
    {
        enableOne(settingsUI);
    }

    public void ChangeQualityPreset(int _index)
    {
        QualitySettings.SetQualityLevel(_index, true);
    }

    public void StartGame()
    {
        GameState.currentState = GameState.LevelStatus.levelInProgress;
        GameState.paused = false;
        SceneManager.LoadScene("Level One");
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
