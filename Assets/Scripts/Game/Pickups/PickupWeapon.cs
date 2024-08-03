using UnityEngine;

public class PickupWeapon : Pickup
{
    public override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if (collision.gameObject.tag == "Player")
        {
            Player player = collision.gameObject.GetComponent<Player>();

            void pickupWeapon(string weapon)
            {
                player.TriggerChangedWeapon(weapon);

                if (GameManager.Instance.GetWeaponCount(weapon) < 1)
                {
                    player.SetWeapon(weapon);
                }

                int maximumWeaponCount = GameManager.Instance.MaximumWeaponCount;

                int weapontCount = GameManager.Instance.GetWeaponCount(weapon);
                // Keep this logic here instead of in UpgradeWeapon as UpgradeWeapon needs to be able to go up to maximum weapon count (on death)
                if (weapontCount < maximumWeaponCount)
                {
                    GameManager.Instance.AddToWeaponCount(weapon);
                    player.UpgradeWeapon(weapon);
                }
            }

            switch (this.gameObject.tag)
            {
                case "PickupShotgun":
                    pickupWeapon("Shotgun");
                    break;
                case "PickupLaser":
                    pickupWeapon("Laser");
                    break;
                case "PickupCannon":
                    pickupWeapon("Cannon");
                    break;
            }
        }
    }
}
