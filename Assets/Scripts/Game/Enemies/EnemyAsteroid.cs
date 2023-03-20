using UnityEngine;

public class EnemyAsteroid : Enemy
{
    public Sprite[] sprites;

    public override void Start()
    {
        base.Start();
        this.RandomiseSprite();
    }

    private void RandomiseSprite()
    {
        this.spriteRenderer.sprite = this.sprites[Random.Range(0, this.sprites.Length)];
    }
}
