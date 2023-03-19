using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public Menu pauseMenu;

    private void Update()
    {
        bool escapePressed = Input.GetKeyDown(KeyCode.Escape);
        bool playerAlive = GameManager.Instance.playerLives >= 1;

        if (escapePressed && playerAlive)
        {
            this.pauseMenu.TogglePause();
        }
    }
}

