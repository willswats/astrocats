using UnityEngine;
using System.Collections.Generic;

public class WeaponShotgun : Weapon
{
    public int projectileCount = 3;
    private List<Projectile> projectiles;

    public override void HandleFire(Vector2 playerPosition, Vector2 playerDirection, Quaternion playerRotation)
    {
        projectiles = new List<Projectile>();

        for (int i = 0; i < projectileCount; i++)
        {
            Vector2 position = playerPosition + playerDirection + new Vector2(i, 0);
            projectiles.Add(Instantiate(this.projectilePrefab, position, playerRotation));
        }

        foreach (Projectile projectile in projectiles)
        {
            Debug.Log(projectiles);
            projectile.SetForce(playerDirection * this.projectileVelocity);
        }
    }
}
