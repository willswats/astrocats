using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public PickupSpawner pointSpawner;
    public Sprite[] sprites;
    public int asteroidScore = 1;
    public int asteroidDamage = 25;
    public float lifeTimeSeconds = 30f;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb2d;
    private Animator anim;

    private void Awake()
    {
        this.rb2d = GetComponent<Rigidbody2D>();
        this.spriteRenderer = GetComponent<SpriteRenderer>();
        this.anim = GetComponent<Animator>();
    }

    private void Start()
    {
        Destroy(this.gameObject, this.lifeTimeSeconds);
        RandomiseSprite();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        string collisionTag = collision.gameObject.tag;
        if (collisionTag == "Projectile")
        {
            rb2d.simulated = false;
            GetComponent<Collider2D>().enabled = false;
            anim.SetTrigger("Explode");
            pointSpawner.Spawn(this.transform.position);
            Destroy(this.gameObject, 0.5f);
            GameManager.Instance.AddPlayerScore(asteroidScore);
        }
        if (collisionTag == "Player")
        {
            Player player = collision.gameObject.GetComponent<Player>();
            player.DamagePlayer(asteroidDamage);
        }
    }

    private void RandomiseSprite()
    {
        this.spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];
    }
}
