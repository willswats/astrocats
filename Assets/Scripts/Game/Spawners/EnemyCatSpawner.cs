using UnityEngine;
using System.Collections.Generic;

public class EnemyCatSpawner : Spawner
{
    public GameObject enemyCatTarget;
    public float enemyCatSpeed = 10f;
    public Enemy[] enemyCatPrefabs;
    private List<Enemy> enemyCats;

    public void DestroyAllEnemyCats()
    {
        foreach (Enemy enemyCat in enemyCats)
        {
            if (enemyCat != null)
            {
                Destroy(enemyCat.gameObject);
            }

        }
    }

    private Enemy GetRandomEnemyCatPrefab()
    {
        Enemy enemyCatPrefab = enemyCatPrefabs[Random.Range(0, enemyCatPrefabs.Length)];
        return enemyCatPrefab;
    }

    private void SpawnEnemyCat()
    {
        BoxCollider2D spawner = this.GetRandomSpawner(this.spawners);
        Vector2 position = this.GetRandomSpawnerPosition(spawner);
        Enemy enemyCatPrefab = this.GetRandomEnemyCatPrefab();

        Enemy enemyCat = Instantiate(enemyCatPrefab, position, Quaternion.identity);
        enemyCats.Add(enemyCat);

        Vector2 direction = this.enemyCatTarget.transform.position - enemyCat.transform.position;
        enemyCat.rb2d.AddForce(direction * this.enemyCatSpeed);
    }

    private void Start()
    {
        enemyCats = new List<Enemy>();
        StartCoroutine(this.SpawnHandler(SpawnEnemyCat));
        StartCoroutine(this.DecreaseSpawnRateSeconds());
    }
}
