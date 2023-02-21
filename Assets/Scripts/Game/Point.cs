using System.Collections;
using UnityEngine;

public class Point : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider2D;

    private void Start()
    {
        this.spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        this.boxCollider2D = gameObject.GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameManager.Instance.UpdateScore(5);
            this.spriteRenderer.enabled = false;
            this.boxCollider2D.enabled = false;
            StartCoroutine(DestroyGameObjectAfterSeconds(1, gameObject));
        }
    }

    IEnumerator DestroyGameObjectAfterSeconds(float seconds, GameObject gameObject)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(gameObject);
    }
}
