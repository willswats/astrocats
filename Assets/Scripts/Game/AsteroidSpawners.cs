using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawners : MonoBehaviour
{
    public Asteroid asteroidPrefab;
    public GameObject[] asteroidSpawners;
    public GameObject asteroidTarget;
    public float spawnRateSeconds = 2f;
    public float trajectorySpeed = 10f;
    public float minAsteroidTorque = 0f;
    public float maxAsteroidTorque = 50f;

    private void Start()
    {
        InvokeRepeating(nameof(Spawn), spawnRateSeconds, spawnRateSeconds);
    }

    private GameObject getRandomAsteroidSpawner()
    {
        GameObject asteroidSpawner = asteroidSpawners[Random.Range(0, asteroidSpawners.Length)];
        return asteroidSpawner;
    }

    private Vector2 GetRandomAsteroidSpawnerPosition(GameObject asteroidSpawner)
    {
        float positionX = Random.Range(asteroidSpawner.transform.position.x, asteroidSpawner.transform.position.y);
        float positionY = Random.Range(asteroidSpawner.transform.position.x, asteroidSpawner.transform.position.y);

        Vector2 position = new Vector2(positionX, positionY);
        return position;
    }

    private Quaternion GetRandomAsteroidRotation()
    {
        Quaternion rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
        return rotation;
    }

    private void Spawn()
    {
        GameObject asteroidSpawner = getRandomAsteroidSpawner();
        Vector2 position = GetRandomAsteroidSpawnerPosition(asteroidSpawner);
        Quaternion rotation = GetRandomAsteroidRotation();

        Asteroid asteroid = Instantiate(asteroidPrefab, position, rotation);
        Vector2 direction = asteroidTarget.transform.position - asteroid.transform.position;
        Rigidbody2D asteroidRb2d = asteroid.GetComponent<Rigidbody2D>();

        asteroidRb2d.AddForce(direction * trajectorySpeed);
        asteroidRb2d.AddTorque(Random.Range(minAsteroidTorque, maxAsteroidTorque));
    }
}
