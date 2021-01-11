using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float TimeRemaining = 65;
    public bool IsTimerRunning = false;
    public TextMeshPro TimeText;
    [SerializeField] float TimeBetweenGames = 600;
    [SerializeField] float TimeOfGame = 90;
    [SerializeField] GameController GameController;
    bool wasGameTime;
    void Start()
    {
        IsTimerRunning = true;
        wasGameTime = true;
    }
    void Update()
    {
        if (IsTimerRunning)
        {
            if (TimeRemaining > 0)
            {
                TimeRemaining -= Time.deltaTime;
                DisplayTime(TimeRemaining);
            }
            else
            {
                IsTimerRunning = false;
                TimeRemaining = 0;
                if (wasGameTime == true)
                {
                    GameController.TriggerCutscene();
                    wasGameTime = false;
                }
                else
                {
                    GameController.TriggerDowntime();
                    wasGameTime = true;
                }
            }
        }
    }
    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        TimeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
    public void ResetGameTimer()
    {
        TimeRemaining = TimeOfGame;
        IsTimerRunning = true;
    }
    public void ResetDowntimeTimer()
    {
        TimeRemaining = TimeBetweenGames;
        IsTimerRunning = true;
    }
}
