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
    [SerializeField] private String timeToComplete;
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
        timeText.text = timeToComplete.ToString();
    }


    public void SetLevelInfo(int _orbsLeft, String _timeToComplete)
    {
        played = true;
        orbsTaken = totalOrbs - _orbsLeft;
        timeToComplete = _timeToComplete;
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
