using UnityEngine;

public class PointSpawner : MonoBehaviour
{
    public Spawner spawner;
    public BoxCollider2D[] pointSpawners;
    public Point pointPrefab;
    public float spawnRateSeconds = 4f;
    public float minAsteroidTorque = 0f;
    public float maxAsteroidTorque = 50f;


    private void Start()
    {
        InvokeRepeating(nameof(Spawn), spawnRateSeconds, spawnRateSeconds);
    }

    private void Spawn()
    {
        BoxCollider2D pointSpawner = spawner.GetRandomSpawner(pointSpawners);
        Vector2 pointPosition = spawner.GetRandomSpawnerPosition(pointSpawner);
        Quaternion pointRotation = spawner.GetRandomRotation();

        Point point = Instantiate(pointPrefab, pointPosition, pointRotation);
        Rigidbody2D pointRb2d = point.GetComponent<Rigidbody2D>();

        pointRb2d.AddTorque(Random.Range(minAsteroidTorque, maxAsteroidTorque));
    }
}
