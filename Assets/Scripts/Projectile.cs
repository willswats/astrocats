using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Rigidbody2D body;
    public float speed = 500f;
    public float lifeTimeSeconds = 10f;

    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }

    public void Project(Vector2 direction)
    {
        body.AddForce(direction * speed);
        Destroy(gameObject, lifeTimeSeconds);
    }
}
