using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI textTime;
    public TextMeshProUGUI[] textPlayerScores;
    public TextMeshProUGUI textPlayerLives;
    public TextMeshProUGUI textPlayerHealth;
    public TextMeshProUGUI textShotgunUpgrades;
    public TextMeshProUGUI textLaserUpgrades;
    public TextMeshProUGUI textCannonUpgrades;
    public Menu gameOverMenu;
    public Button gameOverMenuSelected;
    public static UIManager Instance { get; private set; }

    public void SetTextTime(float time)
    {
        int timeInt = (int)time;
        int minutes = timeInt / 60;
        int seconds = timeInt % 60;
        string timeText = $"{minutes}m{seconds}s";
        this.textTime.text = $"{timeText}";
    }

    public void SetTextPlayerHealth(int health)
    {
        this.textPlayerHealth.text = $"Health: {health.ToString()}";
    }

    public void SetTextPlayerLives(int lives)
    {
        this.textPlayerLives.text = $"Lives: {lives.ToString()}";
    }

    public void SetTextPlayerScore(int score)
    {
        foreach (TextMeshProUGUI textPlayerScore in this.textPlayerScores)
        {
            textPlayerScore.text = $"Score: {score.ToString()}";
        }
    }


    public void SetTextWeaponUpgrades(string weapon)
    {
        switch (weapon)
        {
            case "Shotgun":
                this.textShotgunUpgrades.text = $"Shotgun: {GameManager.Instance.GetWeaponCount(weapon)}";
                break;
            case "Laser":
                this.textLaserUpgrades.text = $"Laser: {GameManager.Instance.GetWeaponCount(weapon)}";
                break;
            case "Cannon":
                this.textCannonUpgrades.text = $"Cannon: {GameManager.Instance.GetWeaponCount(weapon)}";
                break;
        }
    }

    public void ToggleGameOverMenu()
    {
        this.gameOverMenu.TogglePause();
        this.gameOverMenuSelected.Select();
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
}
