using UnityEngine;
using System.Collections.Generic;

public class Weapon : MonoBehaviour
{
    public Projectile projectilePrefab;
    public List<GameObject> projectileSpawnPoints;
    private List<Projectile> projectiles;
    private AudioSource audiosource;

    private void Start()
    {
        this.audiosource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            this.HandleFire();
        }
    }

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

        this.audiosource.Play();
    }
}
