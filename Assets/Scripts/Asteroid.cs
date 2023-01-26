using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public Sprite[] sprites;
    public float trajectorySpeed = 50f;
    public float lifeTimeSeconds = 30f;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb2d;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        Destroy(gameObject, lifeTimeSeconds);
        RandomiseSprite();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Projectile")
        {
            Destroy(gameObject);
        }
    }

    private void RandomiseSprite()
    {
        spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];
    }

    public void SetForce(Vector2 direction)
    {
        rb2d.AddForce(direction * trajectorySpeed);
    }

    public void SetTorque(float torqueSpeed)
    {
        rb2d.AddTorque(torqueSpeed);
    }
}
