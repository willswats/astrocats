using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    public Asteroid asteroidPrefab;
    public float trajectoryVariance = 15f;
    public float spawnRateSeconds = 2f;
    public float spawnDistance = 15f;
    public int spawnAmount = 1;

    private void Start()
    {
        InvokeRepeating(nameof(SpawnAsteroid), spawnRateSeconds, spawnRateSeconds);
    }

    private void SpawnAsteroid()
    {
        for (int i = 0; i < spawnAmount; i++)
        {
            // Spawn location of asteroid
            Vector3 spawnDirection = Random.insideUnitCircle.normalized * spawnDistance;
            Vector3 spawnPoint = transform.position + spawnDirection;

            // Rotation of asteroid
            float variance = Random.Range(-trajectoryVariance, trajectoryVariance);
            Quaternion rotation = Quaternion.AngleAxis(variance, Vector3.forward);

            // Instantiate and set random size and trajectory
            Asteroid asteroid = Instantiate(asteroidPrefab, spawnPoint, rotation);
            asteroid.size = Random.Range(asteroid.minSize, asteroid.maxSize);
            asteroid.SetTrajectory(rotation * -spawnDirection);
        }
    }
}
