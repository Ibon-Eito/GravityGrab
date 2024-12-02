using System;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

[Serializable]
public class Level : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI orbsText;
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private Button button;
    [SerializeField] private TextMeshProUGUI buttonText;


    [Header("LevelInfo")]
    [SerializeField] private int levelNumber;
    [SerializeField] private int orbsTaken;
    [SerializeField] private int totalOrbs;
    [SerializeField] private int secondsToComplete = 61;
    [SerializeField] private int minutesToComplete = 61;
    [SerializeField] private bool unlocked;
    [SerializeField] private bool played;


    void Start()
    {
        buttonText.text = "LEVEL " + levelNumber.ToString();
        if (unlocked)
        {
            button.interactable = true;
            if (!played)
            {
                SetNotPlayedTexts();
            }
            else
            {
                SetPlayedLevelTexts();
            }
        }
        else
        {
            button.interactable = false;
            SetStartTexts();
        }
    }

    private void SetStartTexts()
    {
        orbsText.text = "?/?";
        timeText.text = "-";
    }

    private void SetNotPlayedTexts()
    {
        orbsText.text = "0/" + totalOrbs.ToString();
        timeText.text = "-";
    }

    private void SetPlayedLevelTexts()
    {
        orbsText.text = orbsTaken.ToString() + "/" + totalOrbs.ToString();
       
        timeText.text = $"{minutesToComplete:D2}:{secondsToComplete:D2}";
    }


    public void SetLevelInfo(LevelInfo levelInfo)
    {
        played = levelInfo.played;
        unlocked = levelInfo.unlocked;
        orbsTaken = totalOrbs - levelInfo.orbsLeft;
        minutesToComplete = levelInfo.minutes;
        secondsToComplete = levelInfo.seconds;
    }

    public void UnlockLevel()
    {
        unlocked = true;
    }

    public void GoToLevel()
    {
        GameManager.Instance.GoToLevel(levelNumber);
    }
}
