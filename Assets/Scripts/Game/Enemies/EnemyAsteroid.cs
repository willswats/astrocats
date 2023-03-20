using UnityEngine;

public class EnemyAsteroid : Enemy
{
    public Sprite[] sprites;
    public float size = 1f;
    public float splitEnemyAsteroidSpeed = 100f;

    public override void Start()
    {
        base.Start();
        this.RandomiseSprite();
    }

    private void RandomiseSprite()
    {
        this.spriteRenderer.sprite = this.sprites[Random.Range(0, this.sprites.Length)];
    }

    public void SplitEnemyAsteroid(int count)
    {
        for (var i = 0; i < count; i++)
        {
            Vector2 splitEnemyAsteroidPosition = transform.position;
            splitEnemyAsteroidPosition += Random.insideUnitCircle;

            EnemyAsteroid splitEnemyAsteroid = Instantiate(this, splitEnemyAsteroidPosition, transform.rotation);
            splitEnemyAsteroid.gameObject.transform.localScale = this.gameObject.transform.localScale / 2;

            splitEnemyAsteroid.rb2d.AddForce(Random.insideUnitCircle.normalized * splitEnemyAsteroidSpeed);
        }
    }

    public override void OnCollisionEnter2D(Collision2D collision)
    {
        string collisionTag = collision.gameObject.tag;
        if (collisionTag == "Projectile")
        {
            if (this.collidedProjectile == false)
            {
                if (this.gameObject.transform.localScale.x > 0.5 && this.gameObject.transform.localScale.y > 0.5)
                {
                    this.SplitEnemyAsteroid(2);
                }
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
