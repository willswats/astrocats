using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public Player player;
    public Player currentPlayer;

    public int experiencePoints = 0;
    public int experiencePointsForLevel5 = 1000;
    public int experiencePointsForLevel4 = 750;
    public int experiencePointsForLevel3 = 500;
    public int experiencePointsForLevel2 = 250;
    public int experiencePointsForLevel1 = 100;

    public int playerLevel = 0;
    public int playerLives = 3;

    public bool gamePaused = false;

    private string currentWeapon = "Default";
    public int weaponShotgunCount = 0;
    public int weaponLaserCount = 0;
    public int weaponCannonCount = 0;

    public EnemyAsteroidSpawner enemyAsteroidSpawner;
    public EnemyCatSpawner enemyCatSpawner;

    private List<Pickup> pickups;
    private List<Projectile> projectiles;

    public static GameManager Instance { get; private set; }

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
                return 0;
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
    }

    public int GetExperiencePointsForLevel(int level)
    {
        switch (level)
        {
            case 4:
                return this.experiencePointsForLevel5;
            case 3:
                return this.experiencePointsForLevel4;
            case 2:
                return this.experiencePointsForLevel3;
            case 1:
                return this.experiencePointsForLevel2;
            case 0:
                return this.experiencePointsForLevel1;
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
