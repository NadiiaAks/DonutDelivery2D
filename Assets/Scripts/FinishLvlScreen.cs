using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLvlScreen : MonoBehaviour
{
    public void NextLvlBtn()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.buildIndex + 1);
        Time.timeScale = 1f;
    }

    public void ExitBtn()
    {
        Application.Quit();
    }
}
