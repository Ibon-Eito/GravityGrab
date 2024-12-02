using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
public class GameData
{
    public bool tutorialDone;
    public LevelInfo[] levelInfos;
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Level info")]
    public int currentLevel = 0;
    public int minutes;
    public int seconds;
    public int orbsLeft;

    [Header("Levels")]
    public GameData gameData;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        GameData data = LoadData();
        if (data != null)
            gameData = data;

        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    #region SceneManagement
    public void GoToGameScene()
    {
        SaveData(gameData);
        if (gameData.tutorialDone)
        {
            MenusAudio.Instance.ResumeMenuMusic();
            SceneManager.LoadScene("LevelSelector");
        }
        else
        {
            MenusAudio.Instance.PauseMenuMusic();
            SceneManager.LoadScene("Tutorial");
        }
    }

    public void GoToMenu()
    {
        MenusAudio.Instance.ResumeMenuMusic();
        SceneManager.LoadScene("MainMenu");
    }

    public void GoToCreditsScene()
    {
        MenusAudio.Instance.ResumeMenuMusic();
        SceneManager.LoadScene("Credits");
    }

    public void GoToLevel(int level)
    {
        orbsLeft = 0;
        MenusAudio.Instance.PauseMenuMusic();
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
        minutes = timer.minutes;
        seconds = timer.seconds;
        Orb[] orbs = FindObjectsOfType<Orb>();

        foreach (Orb orb in orbs)
        {
            if (orb.isActiveAndEnabled && !orb.isGrabbed)
            {
                orbsLeft++;
            }
        }
        
        int previousOrbsLeft = gameData.levelInfos[currentLevel-1].orbsLeft;
        int previousMinutes = gameData.levelInfos[currentLevel - 1].minutes;
        int previousSeconds = gameData.levelInfos[currentLevel - 1].seconds;
        if(minutes < previousMinutes)
            gameData.levelInfos[currentLevel-1] = new LevelInfo(true, true,Math.Min(orbsLeft, previousOrbsLeft),seconds,minutes);
        else if(minutes ==  previousMinutes)
            gameData.levelInfos[currentLevel - 1] = new LevelInfo(true, true, Math.Min(orbsLeft, previousOrbsLeft), Math.Min(seconds, previousSeconds), minutes);
        else
            gameData.levelInfos[currentLevel - 1] = new LevelInfo(true, true, Math.Min(orbsLeft, previousOrbsLeft), previousSeconds, previousMinutes);

        if (currentLevel < gameData.levelInfos.Length)
            gameData.levelInfos[currentLevel].unlocked = true;

        GoToGameScene();
    }

    #endregion

    #region Save&Load
    private static void SaveData(GameData data)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/gravitygrab.data";
        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    private static GameData LoadData()
    {
        string path = Application.persistentDataPath + "/gravitygrab.data";
        Debug.Log(path);
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            GameData data = formatter.Deserialize(stream) as GameData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }

    #endregion
}
