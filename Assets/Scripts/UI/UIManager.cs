using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI textTime;
    public TextMeshProUGUI textPlayerExperiencePoints;
    public TextMeshProUGUI textPlayerHealth;

    public TextMeshProUGUI textShotgunUpgrades;
    public TextMeshProUGUI textCannonUpgrades;
    public TextMeshProUGUI textLaserUpgrades;

    public TextMeshProUGUI[] textsTimeEndScreen;

    public Menu gameOverMenu;
    public Button gameOverMenuSelected;
    private AudioSource gameOverMenuAudioSource;

    public Menu winMenu;
    public Button winMenuSelected;
    private AudioSource winMenuAudioSource;

    public static UIManager Instance { get; private set; }

    private string GetPlayerLevelAndExperienceNeeded(int experiencePoints)
    {
        int playerLevel = GameManager.Instance.PlayerLevel;
        int experiencePointsNeededToLevelUp = GameManager.Instance.GetExperiencePointsForLevel(playerLevel);

        return $"{playerLevel} (XP: {experiencePoints}/{experiencePointsNeededToLevelUp})";
    }

    private string GetTimeTextMinutesSeconds(float time)
    {

        int timeInt = (int)time;
        int minutes = timeInt / 60;
        int seconds = timeInt % 60;
        string timeText = $"{minutes}m:{seconds}s";

        return timeText;
    }


    private string GetTimeTextMinutesSecondsMilliseconds(float time)
    {
        int timeInt = (int)time;
        int minutes = timeInt / 60;
        int seconds = timeInt % 60;
        string milliseconds = time.ToString("N").Split(".")[1];

        if (milliseconds.StartsWith("0"))
        {
            milliseconds = milliseconds.Substring(1);
        }

        string timeText = $"{minutes}m:{seconds}s:{milliseconds}ms";

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
        int weaponCount = GameManager.Instance.GetWeaponCount(weapon);
        int maximumWeaponCount = GameManager.Instance.MaximumWeaponCount;

        switch (weapon)
        {
            case "Shotgun":
                this.textShotgunUpgrades.text = $"(1) Shotgun: {weaponCount}/{maximumWeaponCount}";
                break;
            case "Cannon":
                this.textCannonUpgrades.text = $"(2) Cannon: {weaponCount}/{maximumWeaponCount}";
                break;
            case "Laser":
                this.textLaserUpgrades.text = $"(3) Laser: {weaponCount}/{maximumWeaponCount}";
                break;
        }
    }

    public void SetTextAlphaWeapons()
    {
        string currentWeapon = GameManager.Instance.CurrentWeapon;

        switch (currentWeapon)
        {
            case "Shotgun":
                this.textShotgunUpgrades.faceColor = new Color32(255, 255, 255, 255);
                this.textCannonUpgrades.faceColor = new Color32(255, 255, 255, 100);
                this.textLaserUpgrades.faceColor = new Color32(255, 255, 255, 100);
                break;
            case "Cannon":
                this.textShotgunUpgrades.faceColor = new Color32(255, 255, 255, 100);
                this.textCannonUpgrades.faceColor = new Color32(255, 255, 255, 255);
                this.textLaserUpgrades.faceColor = new Color32(255, 255, 255, 100);
                break;
            case "Laser":
                this.textShotgunUpgrades.faceColor = new Color32(255, 255, 255, 100);
                this.textCannonUpgrades.faceColor = new Color32(255, 255, 255, 100);
                this.textLaserUpgrades.faceColor = new Color32(255, 255, 255, 255);
                break;
            default:
                this.textShotgunUpgrades.faceColor = new Color32(255, 255, 255, 100);
                this.textCannonUpgrades.faceColor = new Color32(255, 255, 255, 100);
                this.textLaserUpgrades.faceColor = new Color32(255, 255, 255, 100);
                break;
        }
    }

    public void ToggleGameOverMenu()
    {
        this.gameOverMenu.TogglePause();
        this.gameOverMenuSelected.Select();
        this.gameOverMenuAudioSource.Play();
    }


    public void ToggleWinMenu()
    {
        this.winMenu.TogglePause();
        this.winMenuSelected.Select();
        this.winMenuAudioSource.Play();
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

        this.gameOverMenuAudioSource = this.gameOverMenu.GetComponent<AudioSource>();
        this.gameOverMenuAudioSource.ignoreListenerPause = true;
        this.winMenuAudioSource = this.winMenu.GetComponent<AudioSource>();
        this.winMenuAudioSource.ignoreListenerPause = true;
    }
}
