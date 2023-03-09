using UnityEngine;

public class PickupWeaponShotgun : Pickup
{
    public override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if (collision.gameObject.tag == "Player")
        {
            Player player = collision.gameObject.GetComponent<Player>();
            player.SetWeaponShotgun();
        }
    }
}
