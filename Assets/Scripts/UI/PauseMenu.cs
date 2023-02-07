using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public void Pause()
    {
        if (gameObject.activeSelf)
        {
            gameObject.SetActive(false);
            Time.timeScale = 1f;
        }
        else
        {
            gameObject.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}
