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

            switch (this.gameObject.tag)
            {
                case "PickupShotgun":
                    player.SetWeapon("Shotgun");
                    player.UpgradeWeapon("Shotgun");
                    break;
                case "PickupLaser":
                    player.SetWeapon("Laser");
                    player.UpgradeWeapon("Laser");
                    break;
                case "PickupCannon":
                    player.SetWeapon("Cannon");
                    player.UpgradeWeapon("Cannon");
                    break;
            }
        }
    }
}
