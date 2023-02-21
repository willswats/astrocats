using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public Menu pauseMenu;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            this.pauseMenu.TogglePause();
        }
    }
}

