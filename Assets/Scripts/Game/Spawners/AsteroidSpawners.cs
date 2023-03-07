using UnityEngine;

public class AsteroidSpawners : Spawner
{
    public BoxCollider2D[] asteroidSpawners;
    public Asteroid asteroidPrefab;
    public GameObject asteroidTarget;
    public float spawnRateSeconds = 4f;
    public float trajectorySpeed = 10f;
    public float minAsteroidTorque = 0f;
    public float maxAsteroidTorque = 50f;

    private void Start()
    {
        InvokeRepeating(nameof(this.Spawn), this.spawnRateSeconds, this.spawnRateSeconds);
    }

    private void Spawn()
    {
        BoxCollider2D asteroidSpawner = this.GetRandomSpawner(this.asteroidSpawners);
        Vector2 asteroidPosition = this.GetRandomSpawnerPosition(asteroidSpawner);
        Quaternion asteroidRotation = this.GetRandomRotation();

        Asteroid asteroid = Instantiate(this.asteroidPrefab, asteroidPosition, asteroidRotation);
        Vector2 direction = this.asteroidTarget.transform.position - asteroid.transform.position;
        Rigidbody2D asteroidRb2d = asteroid.GetComponent<Rigidbody2D>();

        asteroidRb2d.AddForce(direction * trajectorySpeed);
        asteroidRb2d.AddTorque(Random.Range(minAsteroidTorque, maxAsteroidTorque));
    }
}
