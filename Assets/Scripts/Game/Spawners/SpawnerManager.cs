using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnerManager : Spawner
{
    public BoxCollider2D[] spawners;
    public Enemy[] enemyPrefabs;
    public Asteroid asteroidPrefab;
    public GameObject asteroidTarget;
    public float asteroidTrajectorySpeed = 10f;
    public float minAsteroidTorque = 0f;
    public float maxAsteroidTorque = 50f;
    public float spawnRateSeconds = 4f;
    private List<Asteroid> asteroids;
    private List<Enemy> enemies;


    public void DestroyAllAsteroids()
    {
        foreach (Asteroid asteroid in this.asteroids)
        {
            if (asteroid != null)
            {
                Destroy(asteroid.gameObject);
            }
        }
    }

    private Enemy GetRandomEnemyPrefab()
    {
        Enemy enemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
        return enemyPrefab;
    }

    private void SpawnEnemy()
    {
        BoxCollider2D spawner = this.GetRandomSpawner(this.spawners);
        Vector2 position = this.GetRandomSpawnerPosition(spawner);
        Enemy enemyPrefab = this.GetRandomEnemyPrefab();

        Enemy enemy = Instantiate(enemyPrefab, position, Quaternion.identity);
        enemies.Add(enemy);
    }

    private void SpawnAsteroid()
    {
        BoxCollider2D spawner = this.GetRandomSpawner(this.spawners);
        Vector2 position = this.GetRandomSpawnerPosition(spawner);
        Quaternion rotation = this.GetRandomRotation();

        Asteroid asteroid = Instantiate(this.asteroidPrefab, position, rotation);
        asteroids.Add(asteroid);
        Vector2 direction = this.asteroidTarget.transform.position - asteroid.transform.position;
        Rigidbody2D asteroidRb2d = asteroid.GetComponent<Rigidbody2D>();

        asteroidRb2d.AddForce(direction * this.asteroidTrajectorySpeed);
        asteroidRb2d.AddTorque(Random.Range(this.minAsteroidTorque, this.maxAsteroidTorque));
    }

    private IEnumerator SpawnHandler()
    {
        while (true)
        {
            yield return new WaitForSeconds(this.spawnRateSeconds);
            SpawnAsteroid();
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
        asteroids = new List<Asteroid>();
        StartCoroutine(SpawnHandler());
        StartCoroutine(DecreaseSpawnRateSeconds());
    }
}
