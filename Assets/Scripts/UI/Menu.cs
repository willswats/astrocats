using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

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
