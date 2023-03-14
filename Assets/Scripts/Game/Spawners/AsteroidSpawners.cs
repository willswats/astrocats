using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AsteroidSpawners : Spawner
{
    public BoxCollider2D[] asteroidSpawners;
    public Asteroid asteroidPrefab;
    public GameObject asteroidTarget;
    public float trajectorySpeed = 10f;
    public float minAsteroidTorque = 0f;
    public float maxAsteroidTorque = 50f;
    public float spawnRateSeconds = 4f;
    private List<Asteroid> asteroids;

    private void Start()
    {
        asteroids = new List<Asteroid>();
        StartCoroutine(SpawnHandler());
        StartCoroutine(DecreaseSpawnRateSeconds());
    }

    public void Spawn()
    {
        BoxCollider2D asteroidSpawner = this.GetRandomSpawner(this.asteroidSpawners);
        Vector2 asteroidPosition = this.GetRandomSpawnerPosition(asteroidSpawner);
        Quaternion asteroidRotation = this.GetRandomRotation();

        Asteroid asteroid = Instantiate(this.asteroidPrefab, asteroidPosition, asteroidRotation);
        asteroids.Add(asteroid);
        Vector2 direction = this.asteroidTarget.transform.position - asteroid.transform.position;
        Rigidbody2D asteroidRb2d = asteroid.GetComponent<Rigidbody2D>();

        asteroidRb2d.AddForce(direction * this.trajectorySpeed);
        asteroidRb2d.AddTorque(Random.Range(this.minAsteroidTorque, this.maxAsteroidTorque));
    }

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

    IEnumerator SpawnHandler()
    {
        while (true)
        {
            yield return new WaitForSeconds(this.spawnRateSeconds);
            Spawn();
        }
    }

    IEnumerator DecreaseSpawnRateSeconds()
    {
        while (true)
        {
            yield return new WaitForSeconds(10f);
            if (this.spawnRateSeconds >= 1f)
            {
                this.spawnRateSeconds -= 0.5f;
            }
        }
    }
}
