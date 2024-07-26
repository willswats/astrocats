using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI textTime;
    public TextMeshProUGUI textPlayerExperiencePoints;
    public TextMeshProUGUI textPlayerHealth;
    public TextMeshProUGUI textShotgunUpgrades;
    public TextMeshProUGUI textLaserUpgrades;
    public TextMeshProUGUI textCannonUpgrades;
    public Menu gameOverMenu;
    public Button gameOverMenuSelected;
    public static UIManager Instance { get; private set; }

    private string GetPlayerLevelAndExperienceNeeded(int experiencePoints)
    {
        int level = 0;
        int experiencePointsNeededToLevelUp = 100 - experiencePoints;
        if (experiencePoints >= 10000)
        {
            level = 5;
            experiencePointsNeededToLevelUp = 0;
        }
        else if (experiencePoints >= 5000)
        {
            level = 4;
            experiencePointsNeededToLevelUp = 10000 - experiencePoints;
        }
        else if (experiencePoints >= 1000)
        {
            level = 3;
            experiencePointsNeededToLevelUp = 5000 - experiencePoints;
        }
        else if (experiencePoints >= 500)
        {
            level = 2;
            experiencePointsNeededToLevelUp = 1000 - experiencePoints;
        }
        else if (experiencePoints >= 100)
        {
            level = 1;
            experiencePointsNeededToLevelUp = 500 - experiencePoints;
        }

        return $"{level} ({experiencePointsNeededToLevelUp})";
    }

    public void SetTextTime(float time)
    {
        int timeInt = (int)time;
        int minutes = timeInt / 60;
        int seconds = timeInt % 60;
        string timeText = $"{minutes}m{seconds}s";
        this.textTime.text = $"{timeText}";
    }

    public void SetTextPlayerHealth(int health, int lives)
    {
        this.textPlayerHealth.text = $"Health: {health.ToString()} ({lives.ToString()})";
    }

    public void SetTextPlayerExperiencePoints(int experiencePoints)
    {
        string playerLevelAndExperienceNeeded = GetPlayerLevelAndExperienceNeeded(experiencePoints);
        textPlayerExperiencePoints.text = $"Level: {playerLevelAndExperienceNeeded}";
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
