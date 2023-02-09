using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour
{
    public float rotationSpeed = 100f;
    private GameManager gameManager;
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider2D;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        boxCollider2D = gameObject.GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            gameManager.UpdateScore(1);
            spriteRenderer.enabled = false;
            boxCollider2D.enabled = false;
            StartCoroutine(DestroyGameObjectAfterSeconds(1, gameObject));
        }
    }

    IEnumerator DestroyGameObjectAfterSeconds(float seconds, GameObject gameObject)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(gameObject);
    }
}
