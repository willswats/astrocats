using UnityEngine;
using System.Collections;

public class EnemyCatWeapon : Weapon
{
    public float projectileSeconds = 1f;
    private Enemy enemyCat;
    private bool parentCollided = false;

    private void Start()
    {
        enemyCat = GetComponentInParent<Enemy>();
        StartCoroutine(this.HandleFireEnemy());
    }

    public IEnumerator HandleFireEnemy()
    {
        while (true)
        {
            yield return new WaitForSeconds(this.projectileSeconds);
            if (parentCollided == false)
            {
                this.HandleFire();
            }
        }
    }

    private void Update()
    {
        if (enemyCat.collidedProjectile == true)
        {
            this.parentCollided = true;
        }
    }
}
