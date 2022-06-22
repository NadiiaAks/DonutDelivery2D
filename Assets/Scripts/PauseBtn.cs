using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseBtn : MonoBehaviour
{
    [SerializeField] GameObject pauseScreen;

    public void Pause()
    {
        pauseScreen.SetActive(true);
        Time.timeScale = 0f;
    }
}
