using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class LevelInfo
{
    public bool unlocked;
    public bool played;
    public int orbsLeft;
    public int seconds;
    public int minutes;

    public LevelInfo(bool _unlocked, bool _played, int _orbsLeft, int _seconds, int _minutes) 
    {
        this.unlocked = _unlocked;
        this.played = _played;
        this.seconds = _seconds;
        this.minutes = _minutes;
        this.orbsLeft = _orbsLeft;
    }
}

public class LevelManager : MonoBehaviour
{
    [SerializeField] Level[] levels;

    void Awake()
    {
        UpdateLevels();
    }

    private void UpdateLevels()
    {
        int cont = 0;
        foreach(LevelInfo level in GameManager.Instance.gameData.levelInfos)
        {
            levels[cont].SetLevelInfo(level);
            cont++;
        }
    }
}
