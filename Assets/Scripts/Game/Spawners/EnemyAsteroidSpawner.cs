using UnityEngine;
using System.Collections.Generic;

public class EnemyAsteroidSpawner : Spawner
{
    public EnemyAsteroid enemyAsteroidPrefab;
    public GameObject enemyAsteroidTarget;
    public float enemyAsteroidTrajectorySpeed = 10f;
    public float minEnemyAsteroidTorque = 0f;
    public float maxEnemyAsteroidTorque = 50f;
    private List<EnemyAsteroid> enemyAsteroids;

    public void DestroyAllEnemyAsteroids()
    {
        foreach (EnemyAsteroid enemyAsteroid in enemyAsteroids)
        {
            enemyAsteroid.DestroyAllSplitAsteroids();
            enemyAsteroid.DestroyPickups();
            if (enemyAsteroid != null)
            {
                Destroy(enemyAsteroid.gameObject);
            }
        }
    }

    private void SpawnEnemyAsteroid()
    {
        BoxCollider2D spawner = this.GetRandomSpawner(this.spawners);
        Vector2 position = this.GetRandomSpawnerPosition(spawner);
        Quaternion rotation = this.GetRandomRotation();

        EnemyAsteroid enemyAsteroid = Instantiate(this.enemyAsteroidPrefab, position, rotation);
        enemyAsteroids.Add(enemyAsteroid);
        Vector2 direction = this.enemyAsteroidTarget.transform.position - enemyAsteroid.transform.position;
        Rigidbody2D enemyAsteroidRb2d = enemyAsteroid.GetComponent<Rigidbody2D>();

        enemyAsteroidRb2d.AddForce(direction * this.enemyAsteroidTrajectorySpeed);
        enemyAsteroidRb2d.AddTorque(Random.Range(this.minEnemyAsteroidTorque, this.maxEnemyAsteroidTorque));
    }

    private void Start()
    {
        enemyAsteroids = new List<EnemyAsteroid>();
        StartCoroutine(this.SpawnHandler(SpawnEnemyAsteroid));
        StartCoroutine(this.DecreaseSpawnRateSeconds());
    }
}
