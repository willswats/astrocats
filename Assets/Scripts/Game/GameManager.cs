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
        this.textPlayerLives.text = this.playerLives.ToString();
        Instantiate(this.player);
    }

    public void UpdateHealthUI(int health)
    {
        this.textPlayerHealth.text = health.ToString();
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
            Instantiate(this.player);
        }
    }

    public void ToggleGameOverMenu()
    {
        this.gameOverMenu.TogglePause();
        this.gameOverMenuSelected.Select();
    }
}
