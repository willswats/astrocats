using UnityEngine;
using System.Collections.Generic;


public class PickupSpawner : Spawner
{
    public Pickup pickupPrefab;
    public float minPickupTorque = 25f;
    public float maxPickupTorque = 50f;
    private List<Pickup> pickups;

    public void DestroyPickups()
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

        Pickup pickup = Instantiate(this.pickupPrefab, position, rotation);
        pickups.Add(pickup);

        Rigidbody2D pickupRb2d = pickup.GetComponent<Rigidbody2D>();
        pickupRb2d.AddTorque(Random.Range(this.minPickupTorque, this.maxPickupTorque));

    }
}
