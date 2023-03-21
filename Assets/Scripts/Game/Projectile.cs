using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float projectileVelocity = 500f;
    public float projectileLifeTimeSeconds = 10f;
    public float destroySelfSeconds = 1f;
    public int damageGiven = 25;
    private Rigidbody2D rb2d;
    private SpriteRenderer spriteRenderer;
    private AudioSource audiosource;

    public void SetForce(Vector2 direction)
    {
        this.rb2d.AddForce(direction * projectileVelocity);
    }

    private void DestroySelf()
    {
        this.spriteRenderer.enabled = false;
        this.rb2d.simulated = false;
        GetComponent<Collider2D>().enabled = false;

        Destroy(this.gameObject, destroySelfSeconds);
    }

    public void DamagePlayer(Collision2D collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();
        player.DamagePlayer(damageGiven);
    }

    private void Awake()
    {
        this.rb2d = GetComponent<Rigidbody2D>();
        this.spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        Destroy(this.gameObject, this.projectileLifeTimeSeconds);
        this.audiosource = GetComponent<AudioSource>();
        this.audiosource.Play();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        string collisionTag = collision.gameObject.tag;
        DestroySelf();
        if (collisionTag == "Player")
        {
            DamagePlayer(collision);
        }
    }
}
