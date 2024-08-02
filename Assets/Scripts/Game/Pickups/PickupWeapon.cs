using UnityEngine;

public class PickupWeapon : Pickup
{
    public override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if (collision.gameObject.tag == "Player")
        {
            Player player = collision.gameObject.GetComponent<Player>();
            // Setting coroutineRunning to false, because occasionally the pickup can cause the
            // coroutine to not end and instead keep coroutineRunning as true, causing the player not to shoot.
            // Stopping all coroutines as when picking up the same weapon it can cause both of the coroutines to be active if the player
            // holds down the mouse button, causing mutliple of the same projectile to be fired.
            PlayerWeapon[] playerWeapons = player.GetComponentsInChildren<PlayerWeapon>();
            foreach (PlayerWeapon playerWeapon in playerWeapons)
            {
                playerWeapon.coroutineRunning = false;
                playerWeapon.StopAllCoroutines();
            }

            void pickupWeapon(string weapon)
            {
                player.SetWeapon(weapon);

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
