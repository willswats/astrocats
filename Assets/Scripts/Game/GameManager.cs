using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    // TODO: only have one player
    public Player player;
    public Player currentPlayer;

    private int _experiencePoints = 0;
    public int ExperiencePoints
    {
        get => _experiencePoints;
        set => _experiencePoints = value;
    }
    private int experiencePointsForLevel4 = 1600;
    private int experiencePointsForLevel3 = 800;
    private int experiencePointsForLevel2 = 400;
    private int experiencePointsForLevel1 = 200;
    private int experiencePointsForLevel0 = 100;


    private int _playerLevel = 0;
    public int PlayerLevel
    {
        get => _playerLevel;
        set => _playerLevel = value;
    }
    private int _playerLives = 3;
    public int PlayerLives
    {
        get => _playerLives;
        private set => _playerLives = value;
    }

    private bool _gamePaused = false;
    public bool GamePaused
    {
        get => _gamePaused;
        set => _gamePaused = value;
    }

    private string _currentWeapon = "Default";
    public string CurrentWeapon
    {
        get => this._currentWeapon;
        set
        {
            switch (value)
            {
                case "Default":
                    this._currentWeapon = "Default";
                    break;
                case "Shotgun":
                    this._currentWeapon = "Shotgun";
                    break;
                case "Laser":
                    this._currentWeapon = "Laser";
                    break;
                case "Cannon":
                    this._currentWeapon = "Cannon";
                    break;
            }
        }
    }

    private int weaponShotgunCount = 0;
    private int weaponLaserCount = 0;
    private int weaponCannonCount = 0;

    private int _maximumWeaponCount = 10;
    public int MaximumWeaponCount
    {
        get => this._maximumWeaponCount;
    }

    public EnemyAsteroidSpawner enemyAsteroidSpawner;
    public EnemyCatSpawner enemyCatSpawner;

    private List<Pickup> pickups;
    private List<Projectile> projectiles;

    public static GameManager Instance { get; private set; }

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
                if (this.weaponShotgunCount < this.MaximumWeaponCount)
                {
                    this.weaponShotgunCount += 1;
                }
                break;
            case "Laser":
                if (this.weaponLaserCount < this.MaximumWeaponCount)
                {
                    this.weaponLaserCount += 1;
                }
                break;
            case "Cannon":
                if (this.weaponCannonCount < this.MaximumWeaponCount)
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

    public void KillPlayer()
    {
        this.PlayerLives -= 1;
        UIManager.Instance.SetTextPlayerHealth(this.player.health, this.PlayerLives);
        if (this.PlayerLives <= 0)
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
        UIManager.Instance.SetTextPlayerExperiencePoints(this.ExperiencePoints);
        UIManager.Instance.SetTextPlayerHealth(this.player.health, this.PlayerLives);
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
