using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public Player player;
    public int playerScore = 0;
    public int playerLives = 3;
    public bool gamePaused = false;
    private string currentWeapon = "Default";
    private int weaponShotgunCount = 0;
    private int weaponLaserCount = 0;
    private int weaponCannonCount = 0;
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
                this.weaponShotgunCount += 1;
                break;
            case "Laser":
                this.weaponLaserCount += 1;
                break;
            case "Cannon":
                this.weaponCannonCount += 1;
                break;
        }
    }

    public int GetPlayerScore()
    {
        return this.playerScore;
    }

    public void AddPlayerScore(int score)
    {
        this.playerScore += score;
    }

    public void KillPlayer()
    {
        this.playerLives -= 1;
        UIManager.Instance.SetTextPlayerLives(playerLives);
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
            Instantiate(this.player);
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
        UIManager.Instance.SetTextPlayerScore(playerScore);
        UIManager.Instance.SetTextPlayerLives(playerLives);
        UIManager.Instance.SetTextWeaponUpgrades("Shotgun");
        UIManager.Instance.SetTextWeaponUpgrades("Laser");
        UIManager.Instance.SetTextWeaponUpgrades("Cannon");
        Instantiate(this.player);
    }

    private void Update()
    {
        UIManager.Instance.SetTextTime(Time.time);
    }
}
