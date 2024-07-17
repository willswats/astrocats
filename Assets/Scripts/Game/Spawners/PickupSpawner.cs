using UnityEngine;
using System.Collections.Generic;


public class PickupSpawner : Spawner
{
    public Pickup pickupPrefab;
    public float minPointTorque = 25f;
    public float maxPointTorque = 50f;
    private List<Pickup> pickups;

    public void DestroyAllPickups()
    {
        foreach (Pickup pickup in pickups)
        {
            if (pickup != null)
            {
                Destroy(pickup.gameObject);
            }
        }
    }

    public void Spawn(Vector2 position)
    {
        Quaternion rotation = this.GetRandomRotation();

        Pickup point = Instantiate(this.pickupPrefab, position, rotation);
        Rigidbody2D pointRb2d = point.GetComponent<Rigidbody2D>();

        pointRb2d.AddTorque(Random.Range(this.minPointTorque, this.maxPointTorque));

        pickups.Add(point);
    }
}
