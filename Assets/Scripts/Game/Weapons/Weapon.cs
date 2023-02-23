using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int projectileVelocity = 1;
    public Projectile projectilePrefab;

    public virtual void HandleFire(Vector2 playerPosition, Vector2 playerDirection, Quaternion playerRotation)
    {
        Vector2 position = playerPosition + playerDirection;
        Projectile projectile = Instantiate(this.projectilePrefab, position, playerRotation);
        projectile.SetForce(playerDirection * this.projectileVelocity);
    }
}
