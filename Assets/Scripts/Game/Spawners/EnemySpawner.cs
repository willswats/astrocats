using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawner : Spawner
{
    public BoxCollider2D[] spawners;
    public float spawnRateSeconds = 4f;

    public EnemyAsteroid enemyAsteroidPrefab;
    public GameObject enemyAsteroidTarget;
    public float enemyAsteroidTrajectorySpeed = 10f;
    public float minEnemyAsteroidTorque = 0f;
    public float maxEnemyAsteroidTorque = 50f;
    private List<EnemyAsteroid> enemyAsteroids;

    public EnemyCat[] enemyCatPrefabs;
    private List<EnemyCat> enemyCats;

    public void DestroyAllAsteroids()
    {
        foreach (EnemyAsteroid enemyAsteroid in this.enemyAsteroids)
        {
            if (enemyAsteroid != null)
            {
                Destroy(enemyAsteroid.gameObject);
            }
        }
    }

    private EnemyCat GetRandomEnemyCatPrefab()
    {
        EnemyCat enemyCatPrefab = enemyCatPrefabs[Random.Range(0, enemyCatPrefabs.Length)];
        return enemyCatPrefab;
    }

    private void SpawnEnemyCat()
    {
        BoxCollider2D spawner = this.GetRandomSpawner(this.spawners);
        Vector2 position = this.GetRandomSpawnerPosition(spawner);
        EnemyCat enemyCatPrefab = this.GetRandomEnemyCatPrefab();

        EnemyCat enemyCat = Instantiate(enemyCatPrefab, position, Quaternion.identity);
        enemyCats.Add(enemyCat);
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

    private IEnumerator SpawnHandler()
    {
        while (true)
        {
            yield return new WaitForSeconds(this.spawnRateSeconds);
            SpawnEnemyAsteroid();
        }
    }

    private IEnumerator DecreaseSpawnRateSeconds()
    {
        while (true)
        {
            yield return new WaitForSeconds(5f);
            if (this.spawnRateSeconds >= 2f)
            {
                this.spawnRateSeconds -= 0.1f;
            }
        }
    }

    private void Start()
    {
        enemyAsteroids = new List<EnemyAsteroid>();
        StartCoroutine(SpawnHandler());
        StartCoroutine(DecreaseSpawnRateSeconds());
    }
}
