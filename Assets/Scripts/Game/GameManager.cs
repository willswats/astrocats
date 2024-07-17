using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Player player;
    public int playerScore = 0;
    public int playerLives = 3;
    public bool gamePaused = false;
    public EnemyAsteroidSpawner enemyAsteroidSpawner;
    public EnemyCatSpawner enemyCatSpawner;
    public PickupSpawner pickupSpawnerPoint;
    public PickupSpawner pickupSpawnerShotgun;
    public PickupSpawner pickupSpawnerLaser;
    public PickupSpawner pickupSpawnerCannon;
    public static GameManager Instance { get; private set; }

    public GameObject GetGameObjectWithTag(GameObject parent, string tag)
    {
        for (int i = 0; i < parent.transform.childCount; i++)
        {
            if (parent.transform.GetChild(i).gameObject.tag == tag)
            {
                return parent.transform.GetChild(i).gameObject;
            }
        }

        return null;
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
            this.pickupSpawnerPoint.DestroyAllPickups();
            this.pickupSpawnerShotgun.DestroyAllPickups();
            this.pickupSpawnerLaser.DestroyAllPickups();
            this.pickupSpawnerCannon.DestroyAllPickups();
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
