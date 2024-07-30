using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public Player player;
    public Player currentPlayer;

    private int experiencePoints = 0;
    private int experiencePointsForLevel4 = 1000;
    private int experiencePointsForLevel3 = 750;
    private int experiencePointsForLevel2 = 500;
    private int experiencePointsForLevel1 = 250;
    private int experiencePointsForLevel0 = 100;

    private int playerLevel = 0;
    private int playerLives = 3;

    private bool gamePaused = false;

    private string currentWeapon = "Default";
    private int weaponShotgunCount = 0;
    private int weaponLaserCount = 0;
    private int weaponCannonCount = 0;

    public EnemyAsteroidSpawner enemyAsteroidSpawner;
    public EnemyCatSpawner enemyCatSpawner;

    private List<Pickup> pickups;
    private List<Projectile> projectiles;

    public static GameManager Instance { get; private set; }

    // WEAPON

    public string GetCurrentWeapon()
    {
        return this.currentWeapon;
    }

    public void SetCurrentWeapon(string weapon)
    {

        switch (weapon)
        {
            case "Default":
                this.currentWeapon = "Default";
                break;
            case "Shotgun":
                this.currentWeapon = "Shotgun";
                break;
            case "Laser":
                this.currentWeapon = "Laser";
                break;
            case "Cannon":
                this.currentWeapon = "Cannon";
                break;
        }
    }

    public int GetWeaponCount(string weapon)
    {

        switch (weapon)
        {
            case "Shotgun":
                return this.weaponShotgunCount;
            case "Laser":
                return this.weaponLaserCount;
            case "Cannon":
                return this.weaponCannonCount;
            default:
                return -1;
        }
    }

    public void AddToWeaponCount(string weapon)
    {

        switch (weapon)
        {
            case "Shotgun":
                if (this.weaponShotgunCount < 20)
                {
                    this.weaponShotgunCount += 1;
                }
                break;
            case "Laser":
                if (this.weaponLaserCount < 20)
                {
                    this.weaponLaserCount += 1;
                }
                break;
            case "Cannon":
                if (this.weaponCannonCount < 20)
                {
                    this.weaponCannonCount += 1;
                }
                break;
        }

        UIManager.Instance.SetTextWeaponUpgrades(weapon);
    }

    public void SetWeaponCount(string weapon, int count)
    {
        int upgrades = 0;
        while (upgrades < count)
        {
            upgrades += 1;
            AddToWeaponCount(weapon);
        }
    }

    // EXPERIENCE AND LEVEL

    public int GetExperiencePointsForLevel(int level)
    {
        switch (level)
        {
            case 4:
                return this.experiencePointsForLevel4;
            case 3:
                return this.experiencePointsForLevel3;
            case 2:
                return this.experiencePointsForLevel2;
            case 1:
                return this.experiencePointsForLevel1;
            case 0:
                return this.experiencePointsForLevel0;
            default:
                return -1;
        }

    }

    public int GetPlayerExperiencePoints()
    {
        return this.experiencePoints;
    }

    public void AddPlayerExperiencePoints(int experiencePoints)
    {
        this.experiencePoints += experiencePoints;
    }

    public void ResetPlayerExperiencePoints()
    {
        this.experiencePoints = 0;
    }

    public int GetPlayerLevel()
    {
        return this.playerLevel;
    }

    public void AddPlayerLevel()
    {
        this.playerLevel += 1;
    }

    public int GetPlayerLives()
    {
        return this.playerLives;
    }

    // OTHER

    public bool GetGamePaused()
    {
        return this.gamePaused;
    }

    public void SetGamePaused(bool gamePaused)
    {
        this.gamePaused = gamePaused;
    }

    public void KillPlayer()
    {
        this.playerLives -= 1;
        UIManager.Instance.SetTextPlayerHealth(this.player.health, this.playerLives);
        if (this.playerLives <= 0)
        {
            UIManager.Instance.ToggleGameOverMenu();
        }
        else
        {
            this.enemyCatSpawner.DestroyAllEnemyCats();
            this.enemyAsteroidSpawner.DestroyAllEnemyAsteroids();
            this.DeleteAllPickups();
            this.DeleteAllProjectiles();
            currentPlayer = Instantiate(this.player);
        }
    }

    public void AddPickup(Pickup pickup)
    {
        pickups.Add(pickup);
    }

    public void DeleteAllPickups()
    {
        foreach (Pickup pickup in pickups)
        {
            if (pickup != null)
            {
                Destroy(pickup.gameObject);
            }
        }
    }

    public void AddProjectile(Projectile projectile)
    {
        projectiles.Add(projectile);
    }

    public void DeleteAllProjectiles()
    {
        foreach (Projectile projectile in projectiles)
        {
            if (projectile != null)
            {
                Destroy(projectile.gameObject);
            }
        }
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

    private void Start()
    {
        pickups = new List<Pickup>();
        projectiles = new List<Projectile>();
        UIManager.Instance.SetTextPlayerExperiencePoints(experiencePoints);
        UIManager.Instance.SetTextPlayerHealth(this.player.health, this.playerLives);
        UIManager.Instance.SetTextWeaponUpgrades("Shotgun");
        UIManager.Instance.SetTextWeaponUpgrades("Laser");
        UIManager.Instance.SetTextWeaponUpgrades("Cannon");
        currentPlayer = Instantiate(this.player);
    }

    private void Update()
    {
        UIManager.Instance.SetTextTime();
        UIManager.Instance.SetTextsTimeEndScreen();
    }
}
