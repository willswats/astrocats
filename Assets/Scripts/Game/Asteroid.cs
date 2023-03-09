using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public PickupSpawner pointSpawner;
    public Sprite[] sprites;
    public int asteroidScore = 1;
    public int asteroidDamage = 25;
    public float lifeTimeSeconds = 30f;
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
        RandomiseSprite();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        string collisionTag = collision.gameObject.tag;
        if (collisionTag == "Projectile")
        {
            DestroyAsteroid(0.5f);
            UpdateAsteroidScore();
        }
        if (collisionTag == "Player")
        {
            DamagePlayer(collision);
        }
    }

    private void DestroyAsteroid(float waitSeconds)
    {
        rb2d.simulated = false;
        GetComponent<Collider2D>().enabled = false;

        anim.SetTrigger("Explode");
        audiosource.Play();
        pointSpawner.Spawn(this.transform.position);

        Destroy(this.gameObject, waitSeconds);
    }

    private void DamagePlayer(Collision2D collision)
    {

        Player player = collision.gameObject.GetComponent<Player>();
        player.DamagePlayer(asteroidDamage);
    }


    private void UpdateAsteroidScore()
    {
        GameManager.Instance.AddPlayerScore(asteroidScore);
        UIManager.Instance.SetTextPlayerScore(asteroidScore);
    }

    private void RandomiseSprite()
    {
        this.spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];
    }
}
