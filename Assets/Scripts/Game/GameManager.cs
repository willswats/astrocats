using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI textPlayerScore;
    public TextMeshProUGUI textPlayerLives;
    public TextMeshProUGUI textPlayerHealth;
    public Button gameOverMenuSelected;
    public Menu gameOverMenu;
    public AsteroidSpawners asteroidSpawners;
    public Player player;
    public int playerScore = 0;
    public int playerLives = 3;
    public int playerHealth = 100;
    public static GameManager Instance { get; private set; }

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
        SetupGame();
    }

    private void SetupGame()
    {
        this.textPlayerLives.text = this.playerLives.ToString();
        this.textPlayerHealth.text = this.playerHealth.ToString();
        Instantiate(this.player);
    }

    public void AddPlayerScore(int score)
    {
        this.playerScore += score;
        this.textPlayerScore.text = score.ToString();
    }

    public void KillPlayer()
    {
        this.playerLives -= 1;
        this.textPlayerLives.text = this.playerLives.ToString();
        if (this.playerLives <= 0)
        {
            ToggleGameOverMenu();
        }
        else
        {
            // TODO: destroy all asteroids
            Instantiate(this.player);
        }
    }

    public void DamagePlayer(int damage)
    {
        if (this.playerHealth <= 0)
        {
            KillPlayer();
        }
        else
        {
            this.playerHealth -= damage;
        }
    }

    public void ToggleGameOverMenu()
    {
        this.gameOverMenu.TogglePause();
        this.gameOverMenuSelected.Select();
    }
}
