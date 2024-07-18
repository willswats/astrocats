using UnityEngine;

public class Pickup : MonoBehaviour, IPickup
{
    public int score = 10;
    public int lifeTimeSeconds = 30;
    private int waitBeforeDestroySeconds = 1;
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider2D;
    private AudioSource audiosource;

    private void PickupDestroy()
    {
        this.spriteRenderer.enabled = false;
        this.boxCollider2D.enabled = false;
        Destroy(this.gameObject, this.waitBeforeDestroySeconds);
    }

    private void UpdatePickupScore()
    {
        GameManager.Instance.AddPlayerScore(this.score);
        UIManager.Instance.SetTextPlayerScore(GameManager.Instance.GetPlayerScore());
    }

    private void Start()
    {
        this.spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
        this.boxCollider2D = this.gameObject.GetComponent<BoxCollider2D>();
        this.audiosource = this.gameObject.GetComponent<AudioSource>();
        Destroy(this.gameObject, lifeTimeSeconds);
    }

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            this.audiosource.Play();
            this.UpdatePickupScore();
            this.PickupDestroy();
        }
    }
}
