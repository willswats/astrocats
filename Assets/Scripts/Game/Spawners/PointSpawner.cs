using UnityEngine;

public class PointSpawner : MonoBehaviour
{
    public Spawner spawner;
    public Point pointPrefab;
    public float spawnRateSeconds = 4f;
    public float minPointTorque = 0f;
    public float maxPointTorque = 50f;
    private BoxCollider2D pointSpawner;

    private void Start()
    {
        InvokeRepeating(nameof(Spawn), spawnRateSeconds, spawnRateSeconds);
        this.pointSpawner = GetComponent<BoxCollider2D>();
    }

    private void Spawn()
    {
        Vector2 pointPosition = this.spawner.GetRandomSpawnerPosition(pointSpawner);
        Quaternion pointRotation = this.spawner.GetRandomRotation();

        Point point = Instantiate(this.pointPrefab, pointPosition, pointRotation);
        Rigidbody2D pointRb2d = point.GetComponent<Rigidbody2D>();

        pointRb2d.AddTorque(Random.Range(this.minPointTorque, this.maxPointTorque));
    }
}
