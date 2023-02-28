using UnityEngine;
using System.Collections.Generic;

public class Weapon : MonoBehaviour
{

    public Projectile projectilePrefab;
    private List<GameObject> projectileSpawnPoints;

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            this.HandleFire();
        }
    }

    public virtual void HandleFire()
    {
        this.projectileSpawnPoints = new List<GameObject>();

        for (int i = 0; i < projectileSpawnPoints.Count; i++)
        {
            Instantiate(this.projectilePrefab, this.projectileSpawnPoints[i].transform.position + transform.up, this.projectileSpawnPoints[i].transform.rotation);
        }

    }
}
