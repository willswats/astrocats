using UnityEngine;

public class Enemy : MonoBehaviour
{
    public PickupSpawner pointSpawner;
    public int scoreGiven = 1;
    public int damageGiven = 25;
    public float lifeTimeSeconds = 30f;
    public float destroySelfSeconds = 2f;
    private bool collidedProjectile = false;

    public SpriteRenderer spriteRenderer;
    private Rigidbody2D rb2d;
    private AudioSource audiosource;

    public virtual void Awake()
    {
        this.spriteRenderer = GetComponent<SpriteRenderer>();
        this.rb2d = GetComponent<Rigidbody2D>();
        this.audiosource = GetComponent<AudioSource>();
    }

    public virtual void Start()
    {
        Destroy(this.gameObject, this.lifeTimeSeconds);
    }

    private void DestroySelf()
    {
        this.spriteRenderer.enabled = false;
        this.rb2d.simulated = false;
        GetComponent<Collider2D>().enabled = false;

        this.audiosource.Play();
        this.pointSpawner.Spawn(this.transform.position);
        Destroy(this.gameObject, destroySelfSeconds);
    }

    private void DamagePlayer(Collision2D collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();
        player.DamagePlayer(damageGiven);
    }

    private void UpdateScore()
    {
        GameManager.Instance.AddPlayerScore(this.scoreGiven);
        UIManager.Instance.SetTextPlayerScore(GameManager.Instance.GetPlayerScore());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        string collisionTag = collision.gameObject.tag;
        if (collisionTag == "Projectile")
        {
            if (this.collidedProjectile == false)
            {
                this.DestroySelf();
                this.UpdateScore();
            }
            this.collidedProjectile = true;
        }
        if (collisionTag == "Player")
        {
            this.DamagePlayer(collision);
        }
    }
}
