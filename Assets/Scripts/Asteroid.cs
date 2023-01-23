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
        // Set random sprite
        spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];

        // Set random rotation on z-axis
        transform.eulerAngles = new Vector3(0, 0, Random.value * 360);
        // Set random scale
        transform.localScale = Vector3.one * size;

        // Set mass relative to size
        body.mass = size;
    }

    public void SetTrajectory(Vector2 direction)
    {
        body.AddForce(direction * speed);

        Destroy(gameObject, lifeTimeSeconds);
    }
}
