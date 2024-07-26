using UnityEngine;
using System.Collections.Generic;
using System;

public class EnemyAsteroid : Enemy
{
    public Sprite[] sprites;
    public float size = 1f;
    public float splitEnemyAsteroidSpeed = 100f;
    private List<EnemyAsteroid> splitEnemyAsteroids;

    public override void Start()
    {
        base.Start();
        this.RandomiseSprite();
        this.splitEnemyAsteroids = new List<EnemyAsteroid>();
    }

    public void DestroyAllSplitAsteroids()
    {
        foreach (EnemyAsteroid splitEnemyAsteroid in splitEnemyAsteroids)
        {
            if (splitEnemyAsteroid != null)
            {
                Destroy(splitEnemyAsteroid.gameObject);
            }
        }
    }
    private void RandomiseSprite()
    {
        this.spriteRenderer.sprite = this.sprites[UnityEngine.Random.Range(0, this.sprites.Length)];
        // Add the polygon colldier after setting the sprite to make it the correct shape
        this.gameObject.AddComponent<PolygonCollider2D>();
    }

    public void SplitEnemyAsteroid(int count)
    {
        double distanceBetweenTwoPoints(float x1, float x2, float y1, float y2)
        {
            double distance = Math.Sqrt(Math.Pow((x1 - x2), 2) + Math.Pow((y1 - y2), 2));
            return distance;

        }

        for (var i = 0; i < count; i++)
        {
            Vector2 splitEnemyAsteroidPosition = this.transform.position;
            Vector2 randomSplitEnemyAsteroidPosition = splitEnemyAsteroidPosition + UnityEngine.Random.insideUnitCircle;

            Vector3 playerPosition = GameManager.Instance.currentPlayer.gameObject.transform.position;

            // find distance between two points (asteroid pos and player pos), if the distance is too small, then re-calculate the split enemy asteroid position
            double distanceBetweenEnemyAsteroidAndPlayer = distanceBetweenTwoPoints(playerPosition.x, randomSplitEnemyAsteroidPosition.x, playerPosition.y, randomSplitEnemyAsteroidPosition.y);
            while (distanceBetweenEnemyAsteroidAndPlayer <= 1)
            {
                randomSplitEnemyAsteroidPosition = splitEnemyAsteroidPosition + UnityEngine.Random.insideUnitCircle;
                distanceBetweenEnemyAsteroidAndPlayer = distanceBetweenTwoPoints(playerPosition.x, randomSplitEnemyAsteroidPosition.x, playerPosition.y, randomSplitEnemyAsteroidPosition.y);
            }

            EnemyAsteroid splitEnemyAsteroid = Instantiate(this, randomSplitEnemyAsteroidPosition, transform.rotation);
            splitEnemyAsteroids.Add(splitEnemyAsteroid);
            splitEnemyAsteroid.gameObject.transform.localScale = this.gameObject.transform.localScale / 2;

            splitEnemyAsteroid.rb2d.AddForce(UnityEngine.Random.insideUnitCircle.normalized * splitEnemyAsteroidSpeed);
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
            }
            this.collidedProjectile = true;
        }
        if (collisionTag == "Player")
        {
            this.DamagePlayer(collision);
        }
    }
}
