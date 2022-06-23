using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] private Slider progressBar;

    private DriverController _driver;

    private int progress;

    private void Awake()
    {
        GameObject driver = GameObject.FindGameObjectWithTag("Driver"); ;
        _driver = driver.GetComponent<DriverController>();
    }

    private void Start()
    {
        progress = _driver.GetCountCatch();
        progressBar.maxValue = _driver.GetCatchInLevel();
        progressBar.value = 0;
    }

    private void Update()
    {
        progressBar.value = progress;
    }
}
