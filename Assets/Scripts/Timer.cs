using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] Slider timer;
    [SerializeField] float timeToLoseSeconds;
    [SerializeField] GameObject gameOverScreen;

    private void Start()
    {
        timer.value = timeToLoseSeconds;
        timer.maxValue = timeToLoseSeconds;
    }

    private void Update()
    {
        TimerWork();
    }

    void TimerWork()
    {
        if(timer.value > timer.minValue)
        {
            timer.value -= Time.deltaTime;
            Debug.Log(timer.value);
        }
        else
        {
            Time.timeScale = 0f;
            gameOverScreen.SetActive(true);
        }
    }
}
