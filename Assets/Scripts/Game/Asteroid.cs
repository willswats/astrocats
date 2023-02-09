using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    private GameManager gameManager;
    public Sprite[] sprites;
    public float lifeTimeSeconds = 30f;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb2d;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        gameManager = FindObjectOfType<GameManager>();
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
            gameManager.UpdateScore(1);
        }
    }

    private void RandomiseSprite()
    {
        spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];
    }
}
