using UnityEngine;

public class PointSpawner : Spawner
{
    public Point pointPrefab;
    public float spawnRateSeconds = 4f;
    public float minPointTorque = 25f;
    public float maxPointTorque = 50f;

    public void Spawn(Vector2 pointPosition)
    {
        Quaternion pointRotation = this.GetRandomRotation();

        Point point = Instantiate(this.pointPrefab, pointPosition, pointRotation);
        Rigidbody2D pointRb2d = point.GetComponent<Rigidbody2D>();

        pointRb2d.AddTorque(Random.Range(this.minPointTorque, this.maxPointTorque));
    }
}
