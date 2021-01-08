using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float timeRemaining = 65;
    public bool timerIsRunning = false;
    public TextMeshPro timeText;
    [SerializeField] float timeBetweenGames = 600;
    [SerializeField] float timeOfGame = 90;
    [SerializeField] GameController gameController;
    bool wasGameTime;
    void Start()
    {
        timerIsRunning = true;
        wasGameTime = true;
    }
    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                timerIsRunning = false;
                timeRemaining = 0;
                if (wasGameTime == true)
                {
                    gameController.TriggerCutscene();
                    wasGameTime = false;
                }
                else
                {
                    gameController.TriggerDowntime();
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

        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
    public void ResetGameTimer()
    {
        timeRemaining = timeOfGame;
        timerIsRunning = true;
    }
    public void ResetDowntimeTimer()
    {
        timeRemaining = timeBetweenGames;
        timerIsRunning = true;
    }
}
