using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MissionNotification : MonoBehaviour
{
    [SerializeField] GameObject missionNotificationScreen;

    private void Start()
    {
        Time.timeScale = 0f;
    }

    public void CloseBtn()
    {
        Time.timeScale = 1f;
        missionNotificationScreen.SetActive(false);
    }
}
