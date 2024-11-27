using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool tutorialDone = false;

    public static GameManager Instance { get; private set; }

    [Header("Level info")]
    public int currentLevel = 0;
    public string timerText;
    public int orbsLeft;

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
        currentLevel = level;
        SceneManager.LoadScene("Level" + level.ToString());
    }

    public void ExitGame()
    {
        //TODO Guardar partida
        Application.Quit();
    }

    public void CompleteLevel()
    {
        Timer timer = FindObjectOfType<Timer>();
        timer.stop = true;
        timerText = timer.timerText.text;
        Orb[] orbs = FindObjectsOfType<Orb>();

        foreach (Orb orb in orbs)
        {
            if (orb.isActiveAndEnabled)
            {
                orbsLeft++;
            }
        }

        GoToGameScene();
    }

    #endregion
}
