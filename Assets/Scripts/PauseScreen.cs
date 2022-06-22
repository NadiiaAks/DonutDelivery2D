using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScreen : MonoBehaviour
{
    public void ContinueBtn()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1.0f;
    }

}
