using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] Toggle soundToggle;

    private void Update()
    {
        if (soundToggle.isOn)
        {
            Debug.Log("Sound ON");
            //код для звука
        }
        else
        {
            Debug.Log("Sound OFF");
        }
    }
    public void StartBtn()
    {
        SceneManager.LoadScene(1);
    }

    public void ExitBtn()
    {
        Application.Quit();
    }
}
