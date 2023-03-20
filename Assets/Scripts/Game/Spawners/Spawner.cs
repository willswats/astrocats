using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class Spawner : MonoBehaviour
{

    public BoxCollider2D[] spawners;
    public float spawnRateSeconds = 4f;

    public BoxCollider2D GetRandomSpawner(BoxCollider2D[] spawners)
    {
        BoxCollider2D spawner = spawners[Random.Range(0, spawners.Length)];
        return spawner;
    }

    public Vector2 GetRandomSpawnerPosition(BoxCollider2D spawner)
    {
        float positionX = Random.Range(spawner.bounds.min.x, spawner.bounds.max.x);
        float positionY = Random.Range(spawner.bounds.min.y, spawner.bounds.max.y);

        Vector2 position = new Vector2(positionX, positionY);
        return position;
    }

    public Quaternion GetRandomRotation()
    {
        Quaternion rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
        return rotation;
    }

    public IEnumerator SpawnHandler(UnityAction method)
    {
        while (true)
        {
            yield return new WaitForSeconds(this.spawnRateSeconds);
            method();
        }
    }

    public IEnumerator DecreaseSpawnRateSeconds()
    {
        while (true)
        {
            yield return new WaitForSeconds(5f);
            if (this.spawnRateSeconds >= 2f)
            {
                this.spawnRateSeconds -= 0.1f;
            }
        }
    }
}
