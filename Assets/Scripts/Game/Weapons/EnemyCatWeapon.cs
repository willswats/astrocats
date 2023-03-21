public class EnemyCatWeapon : Weapon
{
    public float projectileSeconds = 1f;

    private void Start()
    {
        InvokeRepeating(nameof(this.HandleFire), this.projectileSeconds, this.projectileSeconds);
    }
}
