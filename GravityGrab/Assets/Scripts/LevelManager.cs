using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] Level[] levels;

    void Start()
    {
        UpdateCurrentLevel();
    }

    private void UpdateCurrentLevel()
    {
        int levelToUpdate = GameManager.Instance.currentLevel - 1;
        if (levelToUpdate >= 0)
        {
            levels[levelToUpdate].SetLevelInfo(GameManager.Instance.orbsLeft, GameManager.Instance.timerText);
            levels[levelToUpdate + 1].UnlockLevel();
        }
    }
}
