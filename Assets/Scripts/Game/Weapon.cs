using UnityEngine;
using System.Collections.Generic;

public class Weapon : MonoBehaviour
{
    public Projectile projectilePrefab;
    public List<GameObject> projectileSpawnPoints;
    private List<Projectile> projectiles;

    private void HandleFire()
    {
        this.projectiles = new List<Projectile>();

        for (int i = 0; i < this.projectileSpawnPoints.Count; i++)
        {
            Vector2 direction = this.projectileSpawnPoints[i].transform.position;
            Quaternion rotation = this.projectileSpawnPoints[i].transform.rotation;
            projectiles.Add(Instantiate(this.projectilePrefab, direction, rotation));
        }

        foreach (Projectile projectile in this.projectiles)
        {
            projectile.SetForce(transform.up);
        }
    }

    private void Update()
    {
        bool fireOnePressed = Input.GetButtonDown("Fire1");

        if (fireOnePressed && !GameManager.Instance.gamePaused)
        {
            this.HandleFire();
        }
    }
}
