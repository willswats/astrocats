using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public Menu pauseMenu;

    private void Update()
    {
        bool escapePressed = Input.GetKeyDown(KeyCode.Escape);
        bool playerAlive = GameManager.Instance.GetPlayerLives() >= 1;
        bool gameWon = GameManager.Instance.GetPlayerExperiencePoints() >= GameManager.Instance.GetExperiencePointsForLevel(4);

        if (escapePressed && playerAlive && !gameWon)
        {
            this.pauseMenu.TogglePause();
        }
    }
}

