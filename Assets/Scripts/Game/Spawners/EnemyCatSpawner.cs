using UnityEngine;
using System.Collections.Generic;

public class EnemyCatSpawner : Spawner
{
    public GameObject enemyCatTarget;
    public float enemyCatTrajectorySpeed = 10f;
    public EnemyCat[] enemyCatPrefabs;
    public List<GameObject> enemyCatGameObjects;

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
        enemyCatGameObjects.Add(enemyCat.gameObject);
        Vector2 direction = this.enemyCatTarget.transform.position - enemyCat.transform.position;
        Rigidbody2D enemyCatRb2d = enemyCat.GetComponent<Rigidbody2D>();

        enemyCatRb2d.AddForce(direction * this.enemyCatTrajectorySpeed);
    }

    private void Start()
    {
        enemyCatGameObjects = new List<GameObject>();
        StartCoroutine(this.SpawnHandler(SpawnEnemyCat));
        StartCoroutine(this.DecreaseSpawnRateSeconds());
    }
}
