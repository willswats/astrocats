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

    public TextMeshProUGUI[] textsTimeEndScreen;

    public Menu gameOverMenu;
    public Button gameOverMenuSelected;
    public Menu winMenu;
    public Button winMenuSelected;

    public static UIManager Instance { get; private set; }

    private string GetPlayerLevelAndExperienceNeeded(int experiencePoints)
    {
        int playerLevel = GameManager.Instance.GetPlayerLevel();

        int experiencePointsNeededToLevelUp = 100;
        switch (playerLevel)
        {
            case 0:
                experiencePointsNeededToLevelUp = 100;
                break;
            case 1:
                experiencePointsNeededToLevelUp = 500;
                break;
            case 2:
                experiencePointsNeededToLevelUp = 1000;
                break;
            case 3:
                experiencePointsNeededToLevelUp = 2500;
                break;
            case 4:
                experiencePointsNeededToLevelUp = 5000;
                break;
        }

        return $"{playerLevel} (XP: {experiencePoints}/{experiencePointsNeededToLevelUp})";
    }

    private string GetTimeTextMinutesSeconds(float time)
    {

        int timeInt = (int)time;
        int minutes = timeInt / 60;
        int seconds = timeInt % 60;
        string timeText = $"{minutes}m{seconds}s";

        return timeText;
    }


    private string GetTimeTextMinutesSecondsMilliseconds(float time)
    {
        int timeInt = (int)time;
        int minutes = timeInt / 60;
        int seconds = timeInt % 60;
        string milliseconds = time.ToString("N").Split(".")[1];
        string timeText = $"{minutes}m{seconds}s{milliseconds}ms";

        return timeText;
    }

    public void SetTextTime()
    {
        string timeText = GetTimeTextMinutesSeconds(Time.timeSinceLevelLoad);
        this.textTime.text = $"{timeText}";
    }

    public void SetTextsTimeEndScreen()
    {
        string timeString = GetTimeTextMinutesSecondsMilliseconds(Time.timeSinceLevelLoad);
        foreach (TextMeshProUGUI textsTimeEndScreen in this.textsTimeEndScreen)
        {
            textsTimeEndScreen.text = $"Time: {timeString}";
        }
    }

    public void SetTextPlayerHealth(int health, int lives)
    {
        this.textPlayerHealth.text = $"Health: {health.ToString()} (Lives: {lives.ToString()})";
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
                this.textShotgunUpgrades.text = $"Shotgun: {GameManager.Instance.GetWeaponCount(weapon)}/20";
                break;
            case "Laser":
                this.textLaserUpgrades.text = $"Laser: {GameManager.Instance.GetWeaponCount(weapon)}/20";
                break;
            case "Cannon":
                this.textCannonUpgrades.text = $"Cannon: {GameManager.Instance.GetWeaponCount(weapon)}/20";
                break;
        }
    }

    public void ToggleGameOverMenu()
    {
        this.gameOverMenu.TogglePause();
        this.gameOverMenuSelected.Select();
    }


    public void ToggleWinMenu()
    {
        this.winMenu.TogglePause();
        this.winMenuSelected.Select();
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
