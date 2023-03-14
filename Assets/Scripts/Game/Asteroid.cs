using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public PickupSpawner pointSpawner;
    public Sprite[] sprites;
    public int asteroidScore = 1;
    public int asteroidDamage = 25;
    public float lifeTimeSeconds = 30f;
    private bool collidedProjectile = false;
    private Rigidbody2D rb2d;
    private SpriteRenderer spriteRenderer;
    private Animator anim;
    private AudioSource audiosource;

    private void Awake()
    {
        this.rb2d = GetComponent<Rigidbody2D>();
        this.spriteRenderer = GetComponent<SpriteRenderer>();
        this.anim = GetComponent<Animator>();
        this.audiosource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        Destroy(this.gameObject, this.lifeTimeSeconds);
        this.RandomiseSprite();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        string collisionTag = collision.gameObject.tag;
        if (collisionTag == "Projectile")
        {
            if (this.collidedProjectile == false)
            {
                this.DestroyAsteroid();
                this.UpdateAsteroidScore();
            }
            this.collidedProjectile = true;
        }
        if (collisionTag == "Player")
        {
            this.DamagePlayer(collision);
        }
    }

    private void DestroyAsteroid()
    {
        this.rb2d.simulated = false;
        GetComponent<Collider2D>().enabled = false;

        this.anim.SetTrigger("Explode");
        this.audiosource.Play();
        this.pointSpawner.Spawn(this.transform.position);
        Destroy(this.gameObject, 0.5f);
    }

    private void DamagePlayer(Collision2D collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();
        player.DamagePlayer(asteroidDamage);
    }


    private void UpdateAsteroidScore()
    {
        GameManager.Instance.AddPlayerScore(this.asteroidScore);
        UIManager.Instance.SetTextPlayerScore(GameManager.Instance.GetPlayerScore());
    }

    private void RandomiseSprite()
    {
        this.spriteRenderer.sprite = this.sprites[Random.Range(0, this.sprites.Length)];
    }
}
