using UnityEngine;
using UnityEngine.SceneManagement;
using System.Runtime.InteropServices;

public class Menu : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void Back();

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        Time.timeScale = 1f;
    }

    public void ExitGame()
    {
    #if UNITY_WEBGL
        Back();
    #else
        Application.Quit();
    #endif
    }

    public void TogglePause()
    {
        if (gameObject.activeSelf)
        {
            gameObject.SetActive(false);
            Time.timeScale = 1f;
            GameManager.Instance.gamePaused = false;
        }
        else
        {
            gameObject.SetActive(true);
            Time.timeScale = 0f;
            GameManager.Instance.gamePaused = true;
        }
    }
}
