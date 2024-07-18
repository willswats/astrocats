using UnityEngine;

public class PickupSpawner : Spawner
{
    public Pickup pickupPrefab;
    public float minPickupTorque = 25f;
    public float maxPickupTorque = 50f;

    public Pickup Spawn(Vector2 position)
    {
        Quaternion rotation = this.GetRandomRotation();

        Pickup pickup = Instantiate(this.pickupPrefab, position, rotation);
        GameManager.Instance.AddPickup(pickup);

        Rigidbody2D pickupRb2d = pickup.GetComponent<Rigidbody2D>();
        pickupRb2d.AddTorque(Random.Range(this.minPickupTorque, this.maxPickupTorque));

        return pickup;
    }
}
