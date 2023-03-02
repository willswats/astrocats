using UnityEngine;
using System.Collections;

public class Pickup : MonoBehaviour, IPickup
{
    public int pickupScore = 5;
    public int pickupLifeTimeSeconds = 1;
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider2D;

    private void Start()
    {
        this.spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
        this.boxCollider2D = this.gameObject.GetComponent<BoxCollider2D>();
    }

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameManager.Instance.AddPlayerScore(this.pickupScore);
            PickupDestroy();
        }
    }

    private void PickupDestroy()
    {
        this.spriteRenderer.enabled = false;
        this.boxCollider2D.enabled = false;
        StartCoroutine(GameObjectDestroyAfterSeconds(this.pickupLifeTimeSeconds, this.gameObject));
    }

    private IEnumerator GameObjectDestroyAfterSeconds(float seconds, GameObject gameObject)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(gameObject);
    }
}
