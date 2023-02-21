using UnityEngine;

public class PointSpawner : MonoBehaviour
{
    public Spawner spawner;
    public BoxCollider2D[] pointSpawners;
    public Point pointPrefab;
    public float spawnRateSeconds = 4f;
    public float minPointTorque = 0f;
    public float maxPointTorque = 50f;


    private void Start()
    {
        InvokeRepeating(nameof(Spawn), spawnRateSeconds, spawnRateSeconds);
    }

    private void Spawn()
    {
        BoxCollider2D pointSpawner = this.spawner.GetRandomSpawner(this.pointSpawners);
        Vector2 pointPosition = this.spawner.GetRandomSpawnerPosition(pointSpawner);
        Quaternion pointRotation = this.spawner.GetRandomRotation();

        Point point = Instantiate(this.pointPrefab, pointPosition, pointRotation);
        Rigidbody2D pointRb2d = point.GetComponent<Rigidbody2D>();

        pointRb2d.AddTorque(Random.Range(this.minPointTorque, this.maxPointTorque));
    }
}
