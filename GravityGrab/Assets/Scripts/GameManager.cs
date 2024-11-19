using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool tutorialDone = false;

    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    #region SceneManagement
    public void GoToGameScene()
    {
        if (tutorialDone)
        {
            SceneManager.LoadScene("LevelSelector");
        }
        else
        {
            SceneManager.LoadScene("Tutorial");
        }
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void GoToCreditsScene()
    {
        SceneManager.LoadScene("Credits");
    }

    public void GoToLevel(int level)
    {
        SceneManager.LoadScene("Level" + level.ToString());
    }

    public void ExitGame()
    {
        //TODO Guardar partida
        Application.Quit();
    }

    #endregion
}
