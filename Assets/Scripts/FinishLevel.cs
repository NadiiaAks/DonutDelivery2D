using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLevel : MonoBehaviour
{
    [SerializeField] GameObject finishLevelScreen;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Finish")
        {
            Time.timeScale = 0f;
            finishLevelScreen.SetActive(true);
        }
    }
}
