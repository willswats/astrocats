using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        Time.timeScale = 1f;
        AudioListener.pause = false;
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void TogglePause()
    {
        if (gameObject.activeSelf)
        {
            gameObject.SetActive(false);
            Time.timeScale = 1f;
            GameManager.Instance.GamePaused = false;
            AudioListener.pause = false;
        }
        else
        {
            gameObject.SetActive(true);
            Time.timeScale = 0f;
            GameManager.Instance.GamePaused = true;
            AudioListener.pause = true;
        }
    }
}
