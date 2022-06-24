using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    [SerializeField] GameObject gameOverScreen;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Killer")
        {
            gameObject.SetActive(false);
            gameOverScreen.SetActive(true);
        }
    }
}
