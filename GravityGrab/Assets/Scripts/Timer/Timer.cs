using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public bool stop = false;
    public int minutes;
    public int seconds;

    private float elapsedTime;

    void Start()
    {
        elapsedTime = 0;
    }

    void Update()
    {
        if (!stop)
        {
            elapsedTime += Time.deltaTime;

            minutes = Mathf.FloorToInt(elapsedTime / 60f);
            seconds = Mathf.FloorToInt(elapsedTime % 60f);

            timerText.text = $"{minutes:D2}:{seconds:D2}";
        }
    }
}
