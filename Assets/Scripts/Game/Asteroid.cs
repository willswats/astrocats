using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public PickupSpawner pointSpawner;
    public Sprite[] sprites;
    public int asteroidScore = 1;
    public int asteroidDamage = 10;
    public float lifeTimeSeconds = 30f;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb2d;

    private void Awake()
    {
        this.rb2d = GetComponent<Rigidbody2D>();
        this.spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        Destroy(this.gameObject, this.lifeTimeSeconds);
        RandomiseSprite();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Projectile":
                pointSpawner.Spawn(this.transform.position);
                Destroy(this.gameObject);
                GameManager.Instance.AddPlayerScore(asteroidScore);
                break;
            case "Asteroid":
                GameManager.Instance.DamagePlayer(asteroidDamage);
                break;
        }
    }

    private void RandomiseSprite()
    {
        this.spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];
    }
}
