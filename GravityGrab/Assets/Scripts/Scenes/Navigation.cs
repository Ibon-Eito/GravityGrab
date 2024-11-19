using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Navigation : MonoBehaviour
{
    public void GoToGameScene()
    {
        GameManager.Instance.GoToGameScene();
    }

    public void GoToMenu()
    {
        GameManager.Instance.GoToMenu();
    }

    public void GoToCreditsScene()
    {
        GameManager.Instance.GoToCreditsScene();
    }

    public void GoToLevel(int level)
    {
        GameManager.Instance.GoToLevel(level);
    }

    public void ExitGame()
    {
        GameManager.Instance.ExitGame();
    }
}
