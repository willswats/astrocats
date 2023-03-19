using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public Menu pauseMenu;
    public static GameManager Instance { get; private set; }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && GameManager.Instance.playerLives >= 1)
        {
            this.pauseMenu.TogglePause();
        }
    }
}

