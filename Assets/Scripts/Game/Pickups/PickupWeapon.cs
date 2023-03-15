using UnityEngine;

public class PickupWeapon : Pickup
{
    public override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if (collision.gameObject.tag == "Player")
        {
            Player player = collision.gameObject.GetComponent<Player>();

            switch (this.gameObject.tag)
            {
                case "PickupShotgun":
                    player.SetWeapon("Shotgun");
                    break;
                case "PickupLaser":
                    player.SetWeapon("Laser");
                    break;
                case "PickupCannon":
                    player.SetWeapon("Cannnon");
                    break;
            }
        }
    }
}
