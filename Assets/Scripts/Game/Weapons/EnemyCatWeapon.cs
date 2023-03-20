public class EnemyCatWeapon : Weapon
{
    public float projectileSeconds = 1f;

    private void Start()
    {
        InvokeRepeating(nameof(this.HandleFire), projectileSeconds, projectileSeconds);
    }
}
