using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public Menu pauseMenu;

    private void Update()
    {
        bool escapePressed = Input.GetButtonDown("Cancel");
        bool playerAlive = GameManager.Instance.PlayerLives >= 1;
        bool gameWon = GameManager.Instance.ExperiencePoints >= GameManager.Instance.GetExperiencePointsForLevel(4);

        if (escapePressed && playerAlive && !gameWon)
        {
            this.pauseMenu.TogglePause();
        }
    }
}

