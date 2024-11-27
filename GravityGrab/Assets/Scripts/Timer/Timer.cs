using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public bool stop = false;

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

            int minutes = Mathf.FloorToInt(elapsedTime / 60f);
            int seconds = Mathf.FloorToInt(elapsedTime % 60f);

            timerText.text = $"{minutes:D2}:{seconds:D2}";
        }
    }
}
