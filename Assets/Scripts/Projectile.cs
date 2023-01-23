using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 500f;
    public float lifeTimeSeconds = 10f;
    private Rigidbody2D body;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collison)
    {
        Destroy(gameObject);
    }

    public void Project(Vector2 direction)
    {
        body.AddForce(direction * speed);
        Destroy(gameObject, lifeTimeSeconds);
    }
}
