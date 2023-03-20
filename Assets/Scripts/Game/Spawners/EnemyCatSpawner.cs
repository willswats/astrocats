using UnityEngine;
using System.Collections.Generic;

public class EnemyCatSpawner : Spawner
{
    public GameObject enemyCatTarget;
    public float enemyCatTrajectorySpeed = 10f;
    public EnemyCat[] enemyCatPrefabs;
    private List<EnemyCat> enemyCats;

    public void DestroyAllEnemyCats()
    {
        foreach (EnemyCat enemyCat in enemyCats)
        {
            if (enemyCat != null)
            {
                Destroy(enemyCat.gameObject);
            }
        }
    }

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
        enemyCats.Add(enemyCat);

        Vector2 direction = this.enemyCatTarget.transform.position - enemyCat.transform.position;
        enemyCat.rb2d.AddForce(direction * this.enemyCatTrajectorySpeed);
    }

    private void Start()
    {
        enemyCats = new List<EnemyCat>();
        StartCoroutine(this.SpawnHandler(SpawnEnemyCat));
        StartCoroutine(this.DecreaseSpawnRateSeconds());
    }
}
