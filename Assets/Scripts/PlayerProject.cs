using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProject : MonoBehaviour
{
    public Projectile projectilePrefab;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Project();
        }
    }

    void Project()
    {
        Projectile projectile = Instantiate(projectilePrefab, transform.position, transform.rotation);
        projectile.Project(-transform.up);
    }
}
