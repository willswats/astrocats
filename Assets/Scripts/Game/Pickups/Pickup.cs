using UnityEngine;

public class Pickup : MonoBehaviour, IPickup
{
    public int pickupScore = 5;
    public int pickupLifeTimeSeconds = 1;
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider2D;
    private AudioSource audiosource;

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            this.audiosource.Play();
            this.UpdatePickupScore();
            this.PickupDestroy();
        }
    }

    private void Start()
    {
        this.spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
        this.boxCollider2D = this.gameObject.GetComponent<BoxCollider2D>();
        this.audiosource = this.gameObject.GetComponent<AudioSource>();
    }

    private void PickupDestroy()
    {
        this.spriteRenderer.enabled = false;
        this.boxCollider2D.enabled = false;
        Destroy(this.gameObject, this.pickupLifeTimeSeconds);
    }

    private void UpdatePickupScore()
    {
        GameManager.Instance.AddPlayerScore(this.pickupScore);
        UIManager.Instance.SetTextPlayerScore(this.pickupScore);
    }
}
