using UnityEngine;
using System.Collections.Generic;

public class EnemyAsteroidSpawner : Spawner
{
    public EnemyAsteroid enemyAsteroidPrefab;
    public GameObject enemyAsteroidTarget;
    public float enemyAsteroidTrajectorySpeed = 10f;
    public float minEnemyAsteroidTorque = 0f;
    public float maxEnemyAsteroidTorque = 50f;
    public List<GameObject> enemyAsteroidGameObjects;

    private void SpawnEnemyAsteroid()
    {
        BoxCollider2D spawner = this.GetRandomSpawner(this.spawners);
        Vector2 position = this.GetRandomSpawnerPosition(spawner);
        Quaternion rotation = this.GetRandomRotation();

        EnemyAsteroid enemyAsteroid = Instantiate(this.enemyAsteroidPrefab, position, rotation);
        enemyAsteroidGameObjects.Add(enemyAsteroid.gameObject);
        Vector2 direction = this.enemyAsteroidTarget.transform.position - enemyAsteroid.transform.position;
        Rigidbody2D enemyAsteroidRb2d = enemyAsteroid.GetComponent<Rigidbody2D>();

        enemyAsteroidRb2d.AddForce(direction * this.enemyAsteroidTrajectorySpeed);
        enemyAsteroidRb2d.AddTorque(Random.Range(this.minEnemyAsteroidTorque, this.maxEnemyAsteroidTorque));
    }

    private void Start()
    {
        enemyAsteroidGameObjects = new List<GameObject>();
        StartCoroutine(this.SpawnHandler(SpawnEnemyAsteroid));
        StartCoroutine(this.DecreaseSpawnRateSeconds());
    }
}
