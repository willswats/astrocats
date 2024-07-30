using UnityEngine;

public class Enemy : MonoBehaviour
{
    public PickupSpawner pickupSpawner;
    public PickupSpawner pickupSpawnerHealth;
    public int damageGiven = 25;
    public float lifeTimeSeconds = 30f;
    public float destroySelfSeconds = 2f;
    public bool collidedProjectile = false;

    public SpriteRenderer spriteRenderer { get; private set; }
    public Rigidbody2D rb2d { get; private set; }
    private AudioSource audioSource;

    public virtual void Awake()
    {
        this.spriteRenderer = GetComponent<SpriteRenderer>();
        this.rb2d = GetComponent<Rigidbody2D>();
        this.audioSource = GetComponent<AudioSource>();
    }

    public virtual void Start()
    {
        Destroy(this.gameObject, this.lifeTimeSeconds);
    }

    public void DestroySelf()
    {
        this.spriteRenderer.enabled = false;
        this.rb2d.simulated = false;
        GetComponent<Collider2D>().enabled = false;

        this.audioSource.Play();

        int randomNumber = UnityEngine.Random.Range(1, 31);
        if (randomNumber == 1 && pickupSpawnerHealth != null)
        {
            this.pickupSpawnerHealth.Spawn(this.transform.position);
        }
        else
        {
            this.pickupSpawner.Spawn(this.transform.position);
        }


        int childCount = this.transform.childCount;
        for (int i = 0; i < childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }

        Destroy(this.gameObject, this.destroySelfSeconds);
    }

    public void DamagePlayer(Collision2D collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();
        player.DamagePlayer(damageGiven);
    }

    public virtual void OnCollisionEnter2D(Collision2D collision)
    {
        string collisionTag = collision.gameObject.tag;
        if (collisionTag == "Projectile")
        {
            if (this.collidedProjectile == false)
            {
                this.DestroySelf();
            }
            this.collidedProjectile = true;
        }
        if (collisionTag == "Player")
        {
            this.DamagePlayer(collision);
        }
    }
}
