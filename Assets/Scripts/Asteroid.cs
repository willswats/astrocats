using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public Sprite[] sprites;
    public float size = 1f;
    public float minSize = 0.5f;
    public float maxSize = 1.5f;
    public float speed = 50f;
    public float lifeTimeSeconds = 30f;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D body;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        body = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        RandomiseAsteroid();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Projectile")
        {
            if ((size / 2) > minSize)
            {
                Split(2);
            }
            Destroy(gameObject);
        }
    }

    public void SetTrajectory(Vector2 direction)
    {
        body.AddForce(direction * speed);
        Destroy(gameObject, lifeTimeSeconds);
    }


    private void RandomiseAsteroid()
    {
        spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];

        transform.eulerAngles = new Vector3(0, 0, Random.value * 360);
        transform.localScale = Vector3.one * size;

        body.mass = size;
    }

    private void Split(int count)
    {
        for (var i = 0; i < count; i++)
        {
            Vector2 spawnLocation = transform.position;
            spawnLocation += Random.insideUnitCircle * 1;

            Asteroid splitAsteroid = Instantiate(this, spawnLocation, transform.rotation);
            splitAsteroid.size = size / 2;
            splitAsteroid.SetTrajectory(Random.insideUnitCircle.normalized * speed);
        }
    }
}
