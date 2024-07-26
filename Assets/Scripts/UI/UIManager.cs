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
        int level = 0;
        int experiencePointsNeededToLevelUp = 100;
        if (experiencePoints >= 10000)
        {
            level = 5;
            experiencePointsNeededToLevelUp = 0;
        }
        else if (experiencePoints >= 5000)
        {
            level = 4;
            experiencePointsNeededToLevelUp = 10000;
        }
        else if (experiencePoints >= 1000)
        {
            level = 3;
            experiencePointsNeededToLevelUp = 5000;
        }
        else if (experiencePoints >= 500)
        {
            level = 2;
            experiencePointsNeededToLevelUp = 1000;
        }
        else if (experiencePoints >= 100)
        {
            level = 1;
            experiencePointsNeededToLevelUp = 500;
        }

        return $"{level} (XP: {experiencePoints}/{experiencePointsNeededToLevelUp})";
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
