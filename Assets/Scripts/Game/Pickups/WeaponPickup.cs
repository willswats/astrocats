using UnityEngine;

public class WeaponPickup : Pickup
{
    public Weapon weapon;

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if (collision.gameObject.tag == "Player")
        {
            Player player = collision.gameObject.GetComponent<Player>();
            // TODO: change weapon
        }
    }
}
