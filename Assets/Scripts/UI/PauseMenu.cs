using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public Menu pauseMenu;

    private void Update()
    {
        bool escapePressed = Input.GetKeyDown(KeyCode.Escape);
        bool playerAlive = GameManager.Instance.playerLives >= 1;
        bool gameWon = GameManager.Instance.experiencePoints >= 10000;

        if (escapePressed && playerAlive && !gameWon)
        {
            this.pauseMenu.TogglePause();
        }
    }
}

