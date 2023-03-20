using UnityEngine;
using System.Collections;

public class EnemyCat : Enemy
{
    public float projectileSeconds = 1;
    private Weapon weapon;

    public void start()
    {
        this.weapon = this.GetComponentInChildren<Weapon>();
        InvokeRepeating("weapon.HandleFire", this.projectileSeconds, this.projectileSeconds);
        Debug.Log(weapon);
        StartCoroutine(this.ShootHandler());
    }

    public IEnumerator ShootHandler()
    {
        while (true)
        {
            yield return new WaitForSeconds(this.projectileSeconds);
            weapon.HandleFire();
        }
    }
}
