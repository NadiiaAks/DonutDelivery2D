using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] private Slider progressBar;
    [SerializeField] private GameObject slider;
    [SerializeField] private GameObject notification;

    private DriverController _driver;
    private int progress;

    private void Start()
    {
        GameObject driver = GameObject.FindGameObjectWithTag("Driver"); ;
        _driver = driver.GetComponent<DriverController>();
        progressBar.maxValue = _driver.GetCatchInLevel();
        progressBar.value = 0;
    }

    private void Update()
    {
        progressBar.value = _driver.GetCountCatch();

        MakeNotification();
    }

    public void MakeNotification()
    {
        if(progressBar.value == progressBar.maxValue)
        {
            slider.SetActive(false);
            notification.SetActive(true);
        }
    }
}
