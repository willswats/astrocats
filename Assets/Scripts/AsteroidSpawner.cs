using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    public Asteroid asteroidPrefab;
    public float trajectoryVariance = 15f;
    public float spawnRateSeconds = 2f;
    public float spawnDistance = 15f;
    public float spawnAmount = 1;

    private void Start()
    {
        InvokeRepeating(nameof(Spawn), spawnRateSeconds, spawnRateSeconds);
    }

    private Vector3 getSpawnDirection()
    {
        Vector3 spawnDirection = Random.insideUnitCircle.normalized * spawnDistance;
        return spawnDirection;
    }

    private Vector3 getSpawnLocation(Vector3 spawnDirection)
    {
        Vector3 spawnLocation = transform.position + spawnDirection;
        return spawnLocation;
    }

    private Quaternion getRotation()
    {
        float variance = Random.Range(-trajectoryVariance, trajectoryVariance);
        Quaternion rotation = Quaternion.AngleAxis(variance, Vector3.forward);
        return rotation;
    }

    private void InstantiateAsteroid(Vector3 spawnDirection, Vector3 spawnLocation, Quaternion rotation)
    {
        Asteroid asteroid = Instantiate(asteroidPrefab, spawnLocation, rotation);
        asteroid.size = Random.Range(asteroid.minSize, asteroid.maxSize);
        asteroid.SetTrajectory(rotation * -spawnDirection);
    }

    private void Spawn()
    {
        for (int i = 0; i < spawnAmount; i++)
        {
            Vector3 spawnDirection = getSpawnDirection();
            Vector3 spawnLocation = getSpawnLocation(spawnDirection);
            Quaternion rotation = getRotation();

            InstantiateAsteroid(spawnDirection, spawnLocation, rotation);
        }
    }
}
