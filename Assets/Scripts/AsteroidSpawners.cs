using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawners : MonoBehaviour
{
    public Asteroid asteroidPrefab;
    public GameObject[] asteroidSpawners;
    public float spawnRateSeconds = 2f;
    public float minAsteroidTorque = 0;
    public float maxAsteroidTorque = 50;

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

    private Vector2 GetAsteroidDirection(GameObject asteroidSpawner)
    {
        Vector2 direction;

        switch (asteroidSpawner.name)
        {
            case "AsteroidSpawnerTop":
                direction = -transform.up;
                break;
            case "AsteroidSpawnerBottom":
                direction = transform.up;
                break;
            case "AsteroidSpawnerLeft":
                direction = transform.right;
                break;
            case "AsteroidSpawnerRight":
                direction = -transform.right;
                break;
            default:
                throw new System.Exception("Invalid AsteroidSpawner name");
        }

        return direction;
    }

    private void Spawn()
    {
        GameObject asteroidSpawner = getRandomAsteroidSpawner();
        Vector2 position = GetRandomAsteroidSpawnerPosition(asteroidSpawner);
        Quaternion rotation = GetRandomAsteroidRotation();
        Vector2 direction = GetAsteroidDirection(asteroidSpawner);

        Asteroid asteroid = Instantiate(asteroidPrefab, position, rotation);
        asteroid.SetForce(direction);
        asteroid.SetTorque(Random.Range(minAsteroidTorque, maxAsteroidTorque));
    }
}
