using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Player player;
    public int playerScore = 0;
    public int playerLives = 3;
    public AsteroidSpawners asteroidSpawners;
    public static GameManager Instance { get; private set; }

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
        if (this.playerLives <= 0)
        {
            UIManager.Instance.ToggleGameOverMenu();
        }
        else
        {
            this.asteroidSpawners.DestroyAll();
            Instantiate(this.player);
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
        UIManager.Instance.SetTextPlayerScore(playerScore);
        UIManager.Instance.SetTextPlayerLives(playerLives);
        Instantiate(this.player);
    }
}
