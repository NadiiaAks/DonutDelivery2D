using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLvlScreen : MonoBehaviour
{
    [SerializeField] private int nextScene;
    public void NextLvlBtn()
    {
        SceneManager.LoadScene(nextScene);
    }

    public void ExitBtn()
    {
        Application.Quit();
    }
}
